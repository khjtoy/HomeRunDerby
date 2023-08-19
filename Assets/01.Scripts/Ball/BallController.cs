using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public LayerMask layer;
    private bool oneCheck = false;

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

    private void CheckBat()
    {
        Debug.DrawRay(transform.position, transform.forward * 0.8f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.8f, layer))
        {
            oneCheck = true;
            collisionPointZ = transform.position.z;
            FindObjectOfType<PitcherController>().KillBallSequence();
            FindObjectOfType<HitterController>().HittingBallEvent();
        }
    }

    public void Movement()
    {
        Vector3 hitDirection = Quaternion.Euler(0f, DirectionY(), 0f) * hitter.transform.forward;
        ballRigidbody.useGravity = true;
        ballRigidbody.AddForce(hitDirection * 3000f);
        ballRigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);

        StartCoroutine(StopBall(ballRigidbody));
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
}
