using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterController : MonoBehaviour
{
    private Animator hitterAnimator;

    /* Test Ball */
    public GameObject ball;

    /* Parameters Hash */
    private readonly int swingHash = Animator.StringToHash("Swing");
    private void Awake()
    {
        hitterAnimator = this.GetComponent<Animator>();
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

    private void HittingBallEvent()
    {
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        Vector3 hitDirection = Quaternion.Euler(0f, 0f, 0f) * transform.forward;
        ballRigidbody.useGravity = true;
        ballRigidbody.AddForce(hitDirection * 3000f);

        StartCoroutine(StopBall(ballRigidbody));
    }

    IEnumerator StopBall(Rigidbody ballRigidbody)
    {
        yield return new WaitForSeconds(1f);

        // 일정 시간 후에 속도를 감소시켜 공을 멈추도록 합니다.
        ballRigidbody.velocity *= 0.5f;

        Debug.Log(ballRigidbody.velocity);

        if(ballRigidbody.velocity.magnitude > 0)
            StartCoroutine(StopBall(ballRigidbody));
    }
}
