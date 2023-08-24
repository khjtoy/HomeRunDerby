using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    protected IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void DrawGizmos()
    {
        currentState?.DrawGizmos();
    }
}
