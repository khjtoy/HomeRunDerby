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
        Rigidbody ballRigidbody = pitcher.currentBall.GetComponent<Rigidbody>();
        Vector3 hitDirection = Quaternion.Euler(0f, 0f, 0f) * transform.forward;
        ballRigidbody.useGravity = true;
        ballRigidbody.AddForce(hitDirection * 4500f);
        ballRigidbody.AddForce(Vector3.up * 20f, ForceMode.Impulse);

        StartCoroutine(StopBall(ballRigidbody));
    }

    IEnumerator StopBall(Rigidbody ballRigidbody)
    {
        yield return new WaitForSeconds(1f);

        // 일정 시간 후에 속도를 감소시켜 공을 멈추도록 합니다.
        if (ballRigidbody.gameObject != null)
        {
            ballRigidbody.velocity *= 0.5f;

            if (ballRigidbody.velocity.magnitude > 0)
                StartCoroutine(StopBall(ballRigidbody));
        }
    }

    public void ToggleBatCollider()
    {
        bat.enabled = !bat.enabled;
    }
}
