using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PitcherController : MonoBehaviour
{
    [field : SerializeField]
    public PitcherInfoData PitcherInfo { get; private set; }

    private Animator pitcherAnimator;

    /* Parameters Hash */
    private readonly int pitchingHash = Animator.StringToHash("Pitching");

    [SerializeField]
    private Transform ballSpawnParent;
    [SerializeField]
    private Transform ballSpawnPos;

    public GameObject currentBall { get; private set; }
    public float BallPosY { get; private set; }

    private Sequence ballSequence;

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
        UIManager.Instance.ActiveStrikeZone(false);
        UIManager.Instance.ActiveMode(false);

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

        float posX = Random.Range(PitcherInfo.minPosX, PitcherInfo.maxPosX);
        BallPosY = Random.Range(PitcherInfo.minPosY, PitcherInfo.maxPosY);


        Vector3 ballPos = new Vector3(0.25f, BallPosY, 63f);

        UIManager.Instance.ActiveBallCursor(true, ballPos);

        ballSequence = DOTween.Sequence();
        ballSequence.Append(currentBall.transform.DOMove(ballPos, PitcherInfo.ballArrivalT).OnComplete(() => 
        {
            UIManager.Instance.Judgment(ResultState.StrikeOut);
            UIManager.Instance.ActiveCursor(false);
            UIManager.Instance.ActiveBallCursor(false);
            float cnt = UIManager.Instance.AddOutCount();
            if (cnt < 10)
            {
                StartCoroutine("HideBall");
            }
            else
                UIManager.Instance.Result();
        }));
    }

    private IEnumerator HideBall()
    {
        yield return new WaitForSeconds(PitcherInfo.hideBall);
        PoolManager.Instance.Despawn(currentBall);
        EventManager.TriggerEvent("RePitching", new EventParam());
    }

    public void KillBallSequence()
    {
        ballSequence.Kill();
    }

    private void RePitching(EventParam eventParam)
    {
        EventManager.TriggerEvent("EndDefence", new EventParam());

        AudioManager.Instance.Stop("CrowdHit");
        AudioManager.Instance.Play("CrowdNormal");

        StartCoroutine("WaitPitching");
    }

    private IEnumerator WaitPitching()
    {
        CameraManager.Instance.SetOriginCam();

        UIManager.Instance.ActiveStrikeZone(true);
        UIManager.Instance.ActiveMode(true);
        UIManager.Instance.ActiveCursor(true);
        UIManager.Instance.DisableJudmentText();

        yield return new WaitForSeconds(2f);

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
