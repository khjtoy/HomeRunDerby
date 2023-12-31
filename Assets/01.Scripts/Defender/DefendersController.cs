using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendersController : MonoBehaviour
{
    private List<DefenderController> defenders;

    private DefenderController currentDefender;

    private Vector3 selectDefendersPos;
    private Quaternion selectDefendersRot;

    private void Awake()
    {
        defenders = new List<DefenderController>();

        AddDefender();
    }

    private void Start()
    {
        EventManager.StartListening("StartDefence", FindCatchDenfender);
        EventManager.StartListening("EndDefence", ResetDefender);
    }

    private void AddDefender()
    {
        foreach (Transform defender in transform)
        {
            defenders.Add(defender.GetComponent<DefenderController>());
        }
    }

    private void FindCatchDenfender(EventParam eventParam)
    {
        Vector3 predictPos = eventParam.vectorParam;
        DefenderController selectDefender = defenders[0];
        float distance = predictPos.DistanceFlat(selectDefender.transform.position);
        for(int i = 1; i < defenders.Count; i++)
        {
            float tempDistance = predictPos.DistanceFlat(defenders[i].transform.position);

            if(tempDistance < distance)
            {
                distance = tempDistance;
                selectDefender = defenders[i];
            }
        }

        currentDefender = selectDefender;

        selectDefendersPos = currentDefender.transform.position;
        selectDefendersRot = currentDefender.transform.rotation;
        currentDefender.SavePredictPos(predictPos);
        DefenderStateMachine stateMachine = currentDefender.DefenderStateMachine;
        stateMachine.ChangeState(stateMachine.ChaseState);
    }

    private void ResetDefender(EventParam eventParam)
    {
        if (currentDefender != null)
        {
            DefenderStateMachine stateMachine = currentDefender.DefenderStateMachine;
            stateMachine.ChangeState(stateMachine.IdlingState);
            currentDefender.transform.position = selectDefendersPos;
            currentDefender.transform.rotation = selectDefendersRot;
            currentDefender = null;
        }
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("StartDefence", FindCatchDenfender);
        EventManager.StopListening("EndDefence", ResetDefender);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("StartDefence", FindCatchDenfender);
        EventManager.StopListening("EndDefence", ResetDefender);
    }
}
