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

        DrawView();

        FollowBall();
        CheckFlyBall();
        CheckCatchState();
    }

    public override void DrawGizmos()
    {
        base.DrawGizmos();

        // ���� ���� �� �ִ� ����
        Gizmos.DrawWireSphere(defenderTransform.position, defenderInfo.catchDist);
        
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(defenderTransform.position, defenderInfo.flyCatchDist);

    }

    private void FollowBall()
    {
        Vector3 dir = Vector3.zero;
        if(flyTrace)
        {
            // ���� ��ġ�� �̵�
            dir = (stateMachine.Defender.PredictPos - defenderTransform.position).SetY(0).normalized;
        }
        else
        {
            // ���� ���󰡱�
            dir = (Define.Pitcher.currentBall.transform.position - defenderTransform.position).SetY(0).normalized;

            // ȸ�� ����
            if (dir != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                defenderTransform.rotation = Quaternion.Slerp(defenderTransform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }

        // �̵� ���� ����
        stateMachine.Defender.Rigidbody.velocity = dir * defenderInfo.chaseSpeed;
    }

    private void CheckFlyBall()
    {
        if (flyTrace)
        {
            if (IsBallGrounded() || Lowball()) flyTrace = false;

            if (!Lowball() && stateMachine.Defender.PredictPos.DistanceFlat(defenderTransform.position) <= 1f)
            {
                stateMachine.ChangeState(stateMachine.ObserveState);
            }
        }
    }

    private void CheckCatchState()
    {
        if (IsBallGrounded() && (GetBallDistance() <= stateMachine.Defender.DefenderInfo.catchDist) && FindViewTarget())
        {
            stateMachine.ChangeState(stateMachine.CatchState);
        }

        if(!Lowball() && flyTrace && (GetBallDistance() <= stateMachine.Defender.DefenderInfo.flyCatchDist))
        {
            stateMachine.ChangeState(stateMachine.ObserveState);
        }

    }

    public Vector3 DirFromAngle(float angleInDegrees)
    {
        //��ũ�� �¿� ȸ���� ����
        angleInDegrees += defenderTransform.eulerAngles.y;
        //��� ���Ͱ� ��ȯ
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    public void DrawView()
    {
        Vector3 leftBoundary = DirFromAngle(-defenderInfo.viewAngle / 2);
        Vector3 rightBoundary = DirFromAngle(defenderInfo.viewAngle / 2);
        Debug.DrawLine(defenderTransform.position, defenderTransform.position + leftBoundary * defenderInfo.viewDistance, Color.blue);
        Debug.DrawLine(defenderTransform.position, defenderTransform.position + rightBoundary * defenderInfo.viewDistance, Color.blue);
    }

    public bool FindViewTarget()
    {

        Vector3 dirToTarget = (Define.Pitcher.currentBall.transform.position - defenderTransform.position).normalized;

        // �������� �þ߰�/2 > Cos�� => �þ� ���� ����
        if (Vector3.Dot(defenderTransform.forward, dirToTarget) > Mathf.Cos((defenderInfo.viewAngle / 2) * Mathf.Deg2Rad))
        {
            return true;
        }

        return false;
    }
}
