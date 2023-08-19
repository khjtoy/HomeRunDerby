using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterController : MonoBehaviour
{
    private Animator hitterAnimator;

    [SerializeField]
    private BoxCollider bat;
    [SerializeField]
    private PitcherController pitcher;
    [field : SerializeField]
    public HitterInfoData HitterInfo { get; private set; }

    /* Parameters Hash */
    private readonly int swingHash = Animator.StringToHash("Swing");
    private void Awake()
    {
        hitterAnimator = this.GetComponent<Animator>();
        bat.enabled = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Hitting();
        }
    }

    private void Hitting()
    {
        hitterAnimator.SetTrigger(swingHash);
    }

    public void HittingBallEvent()
    {
        pitcher.currentBall.GetComponent<BallController>().Movement();
    }

    public void ToggleBatCollider()
    {
        bat.enabled = !bat.enabled;
    }
}
