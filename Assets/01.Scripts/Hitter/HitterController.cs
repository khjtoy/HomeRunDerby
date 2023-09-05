using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitterController : MonoBehaviour
{
    private Animator hitterAnimator;

    [SerializeField]
    private BoxCollider bat;
    [SerializeField]
    private PitcherController pitcher;
    [field : SerializeField]
    public HitterInfoData HitterInfo { get; private set; }

    public float HDistance { get; private set; }

    /* Parameters Hash */
    private readonly int swingHash = Animator.StringToHash("Swing");
    private void Awake()
    {
        hitterAnimator = this.GetComponent<Animator>();
        bat.enabled = false;

        // 각 HitValue 지점 사이의 수평적인 거리를 계산
        HDistance = HitterInfo.maxHitValue - HitterInfo.minHitValue;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Hitting();
                }
            }
            else
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Hitting();
                }
            }
        }
    }

    private void Hitting()
    {
        hitterAnimator.SetTrigger(swingHash);
    }

    public void ToggleBatCollider()
    {
        bat.enabled = !bat.enabled;
    }
}
