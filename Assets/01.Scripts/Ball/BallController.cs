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
        if (collision.gameObject.CompareTag("HomeRunZone"))
        {
            ResultManager.Instance.AddHRCount();
            Invoke("ResultBall", 1.5f);
            environmentCheck = true;
        }
        else if(collision.gameObject.CompareTag("Environment"))
        {
            ResultManager.Instance.AddOutCount();
            Invoke("ResultBall", 1.5f);

            environmentCheck = true;
        }
    }

    private void ResultBall()
    {
        EventManager.TriggerEvent("RePitching", new EventParam());
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
            CameraManager.Instance.FollowBall(this.transform);
            Movement();
        }
    }

    private void Movement()
    {
        Vector3 hitDirection = Quaternion.Euler(0f, DirectionY(), 0f) * hitter.transform.forward;
        ballRigidbody.useGravity = true;
        ballRigidbody.AddForce(hitDirection * CalculateSwingForce());
        ballRigidbody.AddForce(Vector3.up * CalculateHeightForce(), ForceMode.Impulse);

        StartCoroutine(StopBall(ballRigidbody));
    }

    private float DirectionY()
    {
        float rotY;
        // �о�ġ��(Push-hitting)
        if (collisionPointZ > hitter.HitterInfo.hitStandardValue)
        {
            Debug.Log("�о�ġ��");
            rotY = Random.Range(0f, hitter.HitterInfo.hittingRotY);
        }
        // ���ġ��(Pull-hitting)
        else
        {
            Debug.Log("���ġ��");
            rotY = Random.Range(-hitter.HitterInfo.hittingRotY, 0f);
        }
        return rotY;
    }

    private float CalculatePercent()
    {
        // �󸶳� ������ �ִ��� ���
        float targetHDistance = collisionPointZ - hitter.HitterInfo.minHitValue;

        // ����� ���
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

        // ���� �ð� �Ŀ� �ӵ��� ���ҽ��� ���� ���ߵ��� ��.
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
