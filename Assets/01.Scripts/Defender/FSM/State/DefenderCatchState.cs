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
        float speed = 1f + (stateMachine.Defender.DefenderInfo.speedPercent * magnitude);

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
        Vector3 dir = (Define.Pitcher.currentBall.transform.position - stateMachine.Defender.transform.position).SetY(0).normalized;

        // 이동 방향 설정
        stateMachine.Defender.Rigidbody.velocity = dir * stateMachine.Defender.DefenderInfo.groundSpeed;

        // 회전 설정
        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            stateMachine.Defender.transform.rotation = Quaternion.Slerp(stateMachine.Defender.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    public override void OnAnimationEnterEvent()
    {
        base.OnAnimationEnterEvent();

        moving = false;

        PoolManager.Instance.Despawn(Define.Pitcher.currentBall.gameObject);
    }
}
