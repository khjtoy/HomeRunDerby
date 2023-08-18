using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PitcherController : MonoBehaviour
{
    [SerializeField]
    private PitcherInfoData pitcherInfo;

    private Animator pitcherAnimator;

    /* Parameters Hash */
    private readonly int pitchingHash = Animator.StringToHash("Pitching");

    /* Test */
    public Transform testBall;
    public Transform ballPos;
    public Transform ballParent;

    private void Awake()
    {
        pitcherAnimator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        InvokeRepeating("ThrowBall", 2, 4);
    }

    private void ThrowBall()
    {
        // To Do Ball »ý¼º

        pitcherAnimator.SetBool(pitchingHash, true);
    }

    public void ThrowBallEvent()
    {
        testBall.parent = ballParent;
        testBall.DOMove(ballPos.position, pitcherInfo.ballArrivalT);
    }
}
