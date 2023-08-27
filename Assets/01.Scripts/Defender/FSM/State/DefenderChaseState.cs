using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderChaseState : DefenderBaseState
{
    /* Parameters Hash */
    private readonly int runHash = Animator.StringToHash("Run");

    private bool flyTrace = false;

    public DefenderChaseState(DefenderStateMachine defenderStateMachine) : base(defenderStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        flyTrace = true;
        StartAnimation(runHash);
    }

    public override void Exit()
    {
        base.Exit();
        ResetVelocity();
        StopAnimation(runHash);
    }

    public override void Update()
    {
        base.Update();

        FollowBall();
        CheckFlyBall();
        CheckCatchState();
    }

    public override void DrawGizmos()
    {
        base.DrawGizmos();

        // ���� ���� �� �ִ� ����
        Gizmos.DrawWireSphere(stateMachine.Defender.transform.position, stateMachine.Defender.DefenderInfo.catchDist);
        
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(stateMachine.Defender.transform.position, stateMachine.Defender.DefenderInfo.flyCatchDist);

    }

    private void FollowBall()
    {
        Vector3 dir = Vector3.zero;
        if(flyTrace)
        {
            // ���� ��ġ�� �̵�
            dir = (stateMachine.Defender.PredictPos - stateMachine.Defender.transform.position).SetY(0).normalized;
        }
        else
        {
            // ���� ���󰡱�
            dir = (Define.Pitcher.currentBall.transform.position - stateMachine.Defender.transform.position).SetY(0).normalized;

            // ȸ�� ����
            if (dir != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                stateMachine.Defender.transform.rotation = Quaternion.Slerp(stateMachine.Defender.transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }

        // �̵� ���� ����
        stateMachine.Defender.Rigidbody.velocity = dir * stateMachine.Defender.DefenderInfo.chaseSpeed;
    }

    private void CheckFlyBall()
    {
        if (flyTrace)
        {
            if (IsBallGrounded() || Lowball()) flyTrace = false;

            if (!Lowball() && stateMachine.Defender.PredictPos.DistanceFlat(stateMachine.Defender.transform.position) <= 1f)
            {
                stateMachine.ChangeState(stateMachine.ObserveState);
            }
        }
    }

    private void CheckCatchState()
    {
        if (IsBallGrounded() && (GetBallDistance() <= stateMachine.Defender.DefenderInfo.catchDist))
        {
            stateMachine.ChangeState(stateMachine.CatchState);
        }

        if(!Lowball() && flyTrace && (GetBallDistance() <= stateMachine.Defender.DefenderInfo.flyCatchDist))
        {
            stateMachine.ChangeState(stateMachine.ObserveState);
        }

    }
}
