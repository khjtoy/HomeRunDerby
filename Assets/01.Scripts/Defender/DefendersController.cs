using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendersController : MonoBehaviour
{
    private List<DefenderController> defenders;

    private void Awake()
    {
        defenders = new List<DefenderController>();

        AddDefender();
    }

    private void Start()
    {
        EventManager.StartListening("StartDefence", FindCatchDenfender);
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

        DefenderStateMachine stateMachine = selectDefender.DefenderStateMachine;
        stateMachine.ChangeState(stateMachine.ChaseState);
        Debug.Log(selectDefender.name);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("StartDefence", FindCatchDenfender);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("StartDefence", FindCatchDenfender);
    }
}
