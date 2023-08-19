using UnityEngine;

public class BatController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log(other.transform.position);
            FindObjectOfType<PitcherController>().KillBallSequence();
            FindObjectOfType<HitterController>().HittingBallEvent();
        }
    }
}
