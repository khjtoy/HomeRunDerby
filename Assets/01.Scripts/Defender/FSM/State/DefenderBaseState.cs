using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderBaseState : IState
{
    protected DefenderStateMachine stateMachine;

    public DefenderBaseState(DefenderStateMachine defenderStateMachine)
    {
        stateMachine = defenderStateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void DrawGizmos()
    {

    }

    public virtual void OnAnimationEnterEvent()
    {

    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Defender.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Defender.Animator.SetBool(animationHash, false);
    }

    protected void ChangeSpeedAnimation(float speed)
    {
        stateMachine.Defender.Animator.speed = speed;
    }

    protected void ResetVelocity()
    {
        stateMachine.Defender.Rigidbody.velocity = Vector3.zero;
    }

    protected float GetBallDistance()
    {
        return Define.Pitcher.currentBall.transform.position.DistanceFlat(stateMachine.Defender.transform.position);
    }

    protected bool IsBallGrounded()
    {
        return Define.Pitcher.currentBall.GetComponent<BallController>().Grounded;
    }

    protected bool Lowball()
    {
        return Define.Pitcher.currentBall.transform.position.y <= 3f;
    }

    protected void Out()
    {
        float cnt = UIManager.Instance.AddOutCount();
        if (cnt < 10)
        {
            stateMachine.Defender.StartCoroutine(ResultBall());
        }
        else
            UIManager.Instance.Result();
    }

    private IEnumerator ResultBall()
    {
        UIManager.Instance.Judgment(ResultState.Ground);

        yield return new WaitForSeconds(2f);

        EventManager.TriggerEvent("RePitching", new EventParam());
    }
}
