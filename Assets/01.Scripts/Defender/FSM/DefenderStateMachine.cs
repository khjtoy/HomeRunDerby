using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderStateMachine : StateMachine
{
    public DefenderController Defender { get; }

    public DefenderIdlingState IdlingState { get; }
    public DefenderChaseState ChaseState { get; }
    public DefenderObserveState ObserveState { get; }
    public DefenderCatchState CatchState { get; }

    public DefenderStateMachine(DefenderController defender)
    {
        Defender = defender;

        IdlingState = new DefenderIdlingState(this);
        ChaseState = new DefenderChaseState(this);
        ObserveState = new DefenderObserveState(this);
        CatchState = new DefenderCatchState(this);
    }
}
