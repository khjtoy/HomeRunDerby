using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAnimationEventTrigger : MonoBehaviour
{
    private DefenderController defender;

    private void Awake()
    {
        defender = transform.parent.GetComponent<DefenderController>();
    }

    public void OnAnimationEnterEvent()
    {
        defender.OnAnimationEnterEvent();
    }
}
