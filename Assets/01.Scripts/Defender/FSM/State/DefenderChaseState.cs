using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderChaseState : DefenderBaseState
{
    /* Parameters Hash */
    private readonly int runHash = Animator.StringToHash("Run");

    public DefenderChaseState(DefenderStateMachine defenderStateMachine) : base(defenderStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(runHash);
    }

    public override void Exit()
    {
        base.Exit();
        ResetVelocity();
    }

    public override void Update()
    {
        base.Update();
        FollowBall();
        CheckCatchState();
    }

    public override void DrawGizmos()
    {
        base.DrawGizmos();

        // 공을 잡을 수 있는 범위
        Gizmos.DrawWireSphere(stateMachine.Defender.transform.position, stateMachine.Defender.DefenderInfo.catchDist);
    }

    private void FollowBall()
    {
        Vector3 dir = (Define.Pitcher.currentBall.transform.position - stateMachine.Defender.transform.position).SetY(0).normalized;

        stateMachine.Defender.Rigidbody.velocity = dir * stateMachine.Defender.DefenderInfo.ChaseSpeed;
    }

    private void CheckCatchState()
    {
        if(GetBallDistance() <= stateMachine.Defender.DefenderInfo.catchDist)
        {
            stateMachine.ChangeState(stateMachine.CatchState);
        }
    }
}
