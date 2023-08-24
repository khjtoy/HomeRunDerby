using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public LayerMask layer;
    private bool oneCheck = false;
    private bool environmentCheck = false;

    private Rigidbody ballRigidbody;

    private HitterController hitter;
    private float collisionPointZ;

    private float timeStep;
    private float maxSimulationTime;
    private EventParam ballInfoParam;

    private void Awake()
    {
        timeStep = Define.Pitcher.PitcherInfo.timeStep;
        maxSimulationTime = Define.Pitcher.PitcherInfo.maxSimulationTime;
    }

    private void Start()
    {
        ballRigidbody = this.GetComponent<Rigidbody>();
        hitter = Define.Hitter;
    }

    private void Update()
    {
        if (!oneCheck) CheckBat();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (environmentCheck) return;
        CheckHomerunOrOut(collision);
    }

    private void CheckHomerunOrOut(Collision collision)
    {
        if (collision.gameObject.CompareTag("HomeRunZone"))
        {
            environmentCheck = true;
            UIManager.Instance.AddHRCount();
            AudioManager.Instance.Stop("CrowdNormal");
            AudioManager.Instance.Play("CrowdHit");
            Invoke("ResultBall", 1.5f);
        }
        else if (collision.gameObject.CompareTag("Environment"))
        {
            environmentCheck = true;
            float cnt = UIManager.Instance.AddOutCount();
            if (cnt < 10)
            {
                Invoke("ResultBall", 1.5f);
            }
            else
                UIManager.Instance.Result();
        }
    }

    private void ResultBall()
    {
        //EventManager.TriggerEvent("RePitching", new EventParam());
    }

    private void CheckBat()
    {
        Debug.DrawRay(transform.position, transform.forward * 0.8f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.8f, layer))
        {
            oneCheck = true;
            collisionPointZ = transform.position.z;

            Define.Pitcher.KillBallSequence();

            AudioManager.Instance.Play("Hit");


            CameraManager.Instance.FollowBall(this.transform);
            Movement();
        }
    }

    private void Movement()
    {
        /*        float swingForce = CalculateSwingForce();
                float heightForce = CalculateHeightForce();
                Vector3 hitDirection = Quaternion.Euler(0f, DirectionY(), 0f) * hitter.transform.forward;
                ballRigidbody.useGravity = true;
                ballRigidbody.AddForce(hitDirection * swingForce);
                ballRigidbody.AddForce(Vector3.up * heightForce, ForceMode.Impulse);*/

        // Test
        float swingForce = 3000f;
        float heightForce = 5f;
        Vector3 hitDirection = Quaternion.Euler(0f, 0f, 0f) * hitter.transform.forward;
        ballRigidbody.useGravity = true;
        ballRigidbody.AddForce(hitDirection * swingForce);
        ballRigidbody.AddForce(Vector3.up * heightForce, ForceMode.Impulse);

        StartCoroutine(StopBall(ballRigidbody));

        // 예측된 최종 위치를 계산하여 출력
        Vector3 initialPosition = ballRigidbody.position;
        Vector3 initialVelocity = hitDirection * swingForce + Vector3.up * heightForce;
        ballInfoParam.vectorParam = PredictBallPosition(initialPosition, initialVelocity);

        Debug.Log(ballInfoParam.vectorParam);

        EventManager.TriggerEvent("StartDefence", ballInfoParam);
    }
    private Vector3 PredictBallPosition(Vector3 initialPosition, Vector3 initialVelocity)
    {
        Vector3 position = initialPosition;
        Vector3 velocity = initialVelocity;

        for (float t = 0; t < maxSimulationTime; t += timeStep)
        {
            position += velocity * timeStep;
        }
        return position;
    }

    private float DirectionY()
    {
        float rotY;
        // 밀어치기(Push-hitting)
        if (collisionPointZ > hitter.HitterInfo.hitStandardValue)
        {
            Debug.Log("밀어치기");
            rotY = Random.Range(0f, hitter.HitterInfo.hittingRotY);
        }
        // 당겨치기(Pull-hitting)
        else
        {
            Debug.Log("당겨치기");
            rotY = Random.Range(-hitter.HitterInfo.hittingRotY, 0f);
        }
        return rotY;
    }

    private float CalculatePercent()
    {
        // 얼마나 떨어져 있는지 계산
        float targetHDistance = collisionPointZ - hitter.HitterInfo.minHitValue;

        // 백분율 계산
        float percent = (targetHDistance / hitter.HDistance);

        return percent;
    }

    private float CalculateSwingForce()
    {
        float percent = CalculatePercent();
        float addSwingForce = (hitter.HitterInfo.addswingForce - (hitter.HitterInfo.addswingForce * percent));

        float swingForce = Random.Range(
            hitter.HitterInfo.minSwingForce + addSwingForce,
            hitter.HitterInfo.maxStandardSwingForce + addSwingForce);

        Debug.Log($"CheckDistance:{percent}, Point:{collisionPointZ}, SwingForce:{swingForce}");

        return swingForce;
    }

    private float CalculateHeightForce()
    {
        float percent = CalculatePercent();
        float addHeight = (hitter.HitterInfo.addHeight - (hitter.HitterInfo.addHeight * percent));

        float heightForce = Random.Range(
            hitter.HitterInfo.minheight + addHeight,
            hitter.HitterInfo.maxStandardHeight + addHeight);

        return heightForce;
    }

    IEnumerator StopBall(Rigidbody ballRigidbody)
    {
        yield return new WaitForSeconds(1f);

        // 일정 시간 후에 속도를 감소시켜 공을 멈추도록 함.
        if (ballRigidbody.gameObject != null)
        {
            ballRigidbody.velocity *= 0.5f;

            if (ballRigidbody.velocity.magnitude > 0)
                StartCoroutine(StopBall(ballRigidbody));
        }
    }

    private void HomeRunEvent()
    {
        EventManager.TriggerEvent("RePitching", new EventParam());
    }
}
