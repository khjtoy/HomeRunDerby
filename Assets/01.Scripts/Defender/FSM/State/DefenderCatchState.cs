using UnityEngine;

public class DefenderCatchState : DefenderBaseState
{
    public DefenderCatchState(DefenderStateMachine defenderStateMachine) : base(defenderStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        PoolManager.Instance.Despawn(Define.Pitcher.currentBall);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
