using UnityEngine;

public interface IState
{
    public void Enter();
    public void Update();
    public void Exit();
    public void DrawGizmos();
    public void OnAnimationEnterEvent();
}
