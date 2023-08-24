using UnityEngine;

public class DefenderIdlingState : DefenderBaseState
{
    /* Parameters Hash */
    private readonly int idleHash = Animator.StringToHash("idle");

    public DefenderIdlingState(DefenderStateMachine defenderStateMachine) : base(defenderStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(idleHash);
    }

    public override void Update()
    {
        base.Update();
    }
}
