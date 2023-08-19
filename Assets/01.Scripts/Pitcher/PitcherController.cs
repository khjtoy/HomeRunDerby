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

    [SerializeField]
    private Transform ballSpawnParent;
    [SerializeField]
    private Transform ballSpawnPos;

    public GameObject currentBall { get; private set; }

    private Sequence ballSequence;


    /* Test */
    public Transform ballPos;
    public Transform ballParent;
    public GameObject ballPrefab;

    private void Awake()
    {
        pitcherAnimator = this.GetComponent<Animator>();
        ballSequence = DOTween.Sequence();
    }

    private void Start()
    {
        //ThrowBall();
        InvokeRepeating("ThrowBall", 2, 4);
    }

    private void ThrowBall()
    {
        foreach (Transform child in ballParent)
            Destroy(child.gameObject);

        // To Do Ball PoolManager·Î º¯È¯
        currentBall = Instantiate(ballPrefab, ballSpawnPos.position, Quaternion.identity);
        currentBall.transform.parent = ballSpawnParent;

        pitcherAnimator.SetBool(pitchingHash, true);
    }

    public void ThrowBallEvent()
    {
        currentBall.transform.parent = ballParent;
        currentBall.transform.rotation = Quaternion.identity;

        ballSequence = DOTween.Sequence();
        ballSequence.Append(currentBall.transform.DOMove(ballPos.position, pitcherInfo.ballArrivalT));
    }

    public void KillBallSequence()
    {
        ballSequence.Kill();
    }
}
