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
    public Transform ballParent;

    private void Awake()
    {
        pitcherAnimator = this.GetComponent<Animator>();
        ballSequence = DOTween.Sequence();
    }

    private void Start()
    {
        EventManager.StartListening("RePitching", RePitching);

        Invoke("ThrowBall", 2);
        //InvokeRepeating("ThrowBall", 2, 4);
    }

    private void ThrowBall()
    {
        currentBall = PoolManager.Instance.GetPooledObject((int)Define.PooledObject.Ball);
        currentBall.transform.position = ballSpawnPos.position;
        currentBall.transform.rotation = Quaternion.identity;
        currentBall.transform.parent = ballSpawnParent;

        currentBall.SetActive(true);

        pitcherAnimator.SetBool(pitchingHash, true);
    }

    public void ThrowBallEvent()
    {
        currentBall.transform.parent = PoolManager.Instance.transform;
        currentBall.transform.rotation = Quaternion.identity;

        Vector3 ballPos = new Vector3(
            Random.Range(pitcherInfo.minPosX, pitcherInfo.maxPosX),
            1.67f, 63f);

        ballSequence = DOTween.Sequence();
        ballSequence.Append(currentBall.transform.DOMove(ballPos, pitcherInfo.ballArrivalT).OnComplete(() => 
        {
            ResultManager.Instance.AddOutCount();
            StartCoroutine("HideBall");
            EventManager.TriggerEvent("RePitching", new EventParam());
        }));
    }

    private IEnumerator HideBall()
    {
        yield return new WaitForSeconds(pitcherInfo.hideBall);
        PoolManager.Instance.Despawn(currentBall);
    }

    public void KillBallSequence()
    {
        ballSequence.Kill();
    }

    private void RePitching(EventParam eventParam)
    {
        StartCoroutine("WaitPitching");
    }

    private IEnumerator WaitPitching()
    {
        CameraManager.Instance.SwitchBallCam(false);

        yield return new WaitForSeconds(4f);

        ThrowBall();
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("RePitching", RePitching);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("RePitching", RePitching);
    }
}
