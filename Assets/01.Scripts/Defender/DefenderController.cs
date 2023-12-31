using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderController : MonoBehaviour
{
    [field: SerializeField] 
    public DefenderInfoData DefenderInfo { get; private set; }

    [field: SerializeField]
    public Transform BallPos { get; private set; }

    [field: SerializeField]
    public Transform Glove { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    // Save 
    public Vector3 PredictPos { get; private set; }

    public DefenderStateMachine DefenderStateMachine { get; private set; }

    private void Awake()
    {
        DefenderStateMachine = new DefenderStateMachine(this);

        Rigidbody = GetComponentInChildren<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        // 기본 상태를 Idling로 설정
        DefenderStateMachine.ChangeState(DefenderStateMachine.IdlingState);
    }


    private void Update()
    {
        DefenderStateMachine.Update();
    }

    private void OnDrawGizmos()
    {
        if(DefenderStateMachine != null)
            DefenderStateMachine.DrawGizmos();
    }

    public void OnAnimationEnterEvent()
    {
        DefenderStateMachine.OnAnimationEnterEvent();
    }

    public void SavePredictPos(Vector3 predictPos)
    {
        PredictPos = predictPos;
    }
}
