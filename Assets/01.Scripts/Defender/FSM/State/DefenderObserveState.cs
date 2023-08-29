using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DefenderObserveState : DefenderBaseState
{
    private readonly int flyReadyHash = Animator.StringToHash("FlyReady");
    private readonly int flyCatchHash = Animator.StringToHash("FlyCatch");

    private bool follow = true;
    public DefenderObserveState(DefenderStateMachine defenderStateMachine) : base(defenderStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(flyReadyHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(flyReadyHash);
    }

    public override void Update()
    {
        base.Update();

        if (IsBallGrounded() || (follow && Lowball()))
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
        }

        if(follow) Move();
        else
        {
            Catch();
        }
    }

    private void Move()
    {
        // 야구공의 예상 위치 계산
        Vector3 anticipatedVelocity = Define.Pitcher.currentBall.GetComponent<Rigidbody>().velocity.normalized * 10f;
        Vector3 anticipatedPosition = Define.Pitcher.currentBall.transform.position + anticipatedVelocity;

        if (anticipatedPosition.y > 2f)
        {
            /*            GameObject h2 = PoolManager.Instance.GetPooledObject(1);

                        h2.transform.position = anticipatedPosition;
                        h2.SetActive(true);*/

            Vector3 moveDirection = (anticipatedPosition - stateMachine.Defender.Rigidbody.position).normalized;
            moveDirection.y = 0f;

            moveDirection *= stateMachine.Defender.DefenderInfo.flySpeed;
            moveDirection.x += stateMachine.Defender.DefenderInfo.offsetX;

            // 수비자 이동
            stateMachine.Defender.Rigidbody.velocity = moveDirection;
        }
        else
        {
            follow = false;
            ResetVelocity();
        }
    }
    private void Catch()
    {
        if (Vector3.Distance(stateMachine.Defender.Glove.position, Define.Pitcher.currentBall.transform.position) <= 1f)
        {
            Debug.Log(Define.Pitcher.currentBall.transform.position.y);

            StartAnimation(flyCatchHash);
            StopAnimation(flyReadyHash);

            ResetVelocity();

            Define.Pitcher.currentBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Define.Pitcher.currentBall.GetComponent<Rigidbody>().isKinematic = true;
            Define.Pitcher.currentBall.transform.DOMove(stateMachine.Defender.BallPos.position, 0.3f);
            Define.Pitcher.currentBall.transform.parent = stateMachine.Defender.BallPos.transform;
            follow = false;

            Out();

        }
    }
}
