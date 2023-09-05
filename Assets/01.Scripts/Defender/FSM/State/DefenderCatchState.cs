using UnityEngine;

public class DefenderCatchState : DefenderBaseState
{
    /* Parameters Hash */
    private readonly int groundCatchHash = Animator.StringToHash("GroundCatch");

    private bool moving = true;

    public DefenderCatchState(DefenderStateMachine defenderStateMachine) : base(defenderStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();


        float magnitude = Define.Pitcher.currentBall.GetComponent<Rigidbody>().velocity.magnitude;
        float speed = 1f + (defenderInfo.speedPercent * magnitude);

        StartAnimation(groundCatchHash);
        ChangeSpeedAnimation(speed);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(groundCatchHash);
    }

    public override void Update()
    {
        base.Update();

        if(moving)
            FollowBall();
    }

    private void FollowBall()
    {
        Vector3 dir = (Define.Pitcher.currentBall.transform.position - defenderTransform.position).SetY(0).normalized;

        // 이동 방향 설정
        stateMachine.Defender.Rigidbody.velocity = dir * defenderInfo.groundSpeed;

        // 회전 설정
        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            defenderTransform.rotation = Quaternion.Slerp(defenderTransform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    public override void OnAnimationEnterEvent()
    {
        base.OnAnimationEnterEvent();

        moving = false;

        PoolManager.Instance.Despawn(Define.Pitcher.currentBall.gameObject);

        Out();
    }
}
