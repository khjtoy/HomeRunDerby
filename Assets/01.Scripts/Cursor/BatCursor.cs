using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCursor : MonoBehaviour
{
    private RectTransform cursorTransform;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float minPosY = 0.2f;
    [SerializeField]
    private float maxPosY = 2.4f;
    [SerializeField]
    private float upOffset = 23;
    [SerializeField]
    private float downOffset = 17;
    [SerializeField]
    private Transform bat;
    [SerializeField]
    private Joystick joystick;

    private float basePosY;

    private void Awake()
    {
        cursorTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        basePosY = cursorTransform.position.y;
    }

    private void Update()
    {
        if (GameManager.Instance.AutoMode)
            AutoMove();
        else
            CursorMove();
    }

    private void AutoMove()
    {
        Vector3 pos = cursorTransform.position;

        if (Define.Pitcher.currentBall != null)
        {
            if (Define.Pitcher.BallPosY < 1.55f)
                pos.y = Define.Pitcher.BallPosY - 0.4f;
            else if (Define.Pitcher.BallPosY > 1.7f)
                pos.y = Define.Pitcher.BallPosY + 0.1f;
            else
                pos.y = Define.Pitcher.BallPosY - 0.2f;
        }

        pos.y = Mathf.Clamp(pos.y, minPosY, maxPosY);

        // 기본 크기보다 크면(-180(Base) <)
        if (pos.y >= basePosY)
        {
            float check = -180 - ((pos.y - basePosY) * upOffset);
            bat.localRotation = Quaternion.Euler(check, 0, 0);
            cursorTransform.localRotation = Quaternion.Euler(0f, 0f, -21 + ((pos.y - basePosY) * 20));
        }
        else
        {
            float check = -180 + ((basePosY - pos.y) * downOffset);
            bat.localRotation = Quaternion.Euler(check, 0, 0);
            cursorTransform.localRotation = Quaternion.Euler(0f, 0f, -21 - ((basePosY - pos.y) * 15));
        }


        cursorTransform.position = pos;
    }

    private void CursorMove()
    {
        // float v = Input.GetAxisRaw("Vertical");

        float v = joystick.Vertical;

        Vector3 pos = cursorTransform.position;

        pos.y += v * speed * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, minPosY, maxPosY);

        // 기본 크기보다 크면(-180(Base) <)
        if(pos.y >= basePosY)
        {
            float check = -180 - ((pos.y - basePosY) * upOffset);
            // Debug.Log($"PosY:{pos.y}, check:{check}");
            bat.localRotation = Quaternion.Euler(check, 0, 0);
            cursorTransform.localRotation = Quaternion.Euler(0f, 0f, -21 + ((pos.y - basePosY) * 20));
        }
        else
        {
            float check = -180 + ((basePosY - pos.y) * downOffset);
            // Debug.Log($"PosY:{pos.y}, check:{check}");
            bat.localRotation = Quaternion.Euler(check, 0, 0);
            cursorTransform.localRotation = Quaternion.Euler(0f, 0f, -21 - ((basePosY - pos.y) * 15));
        }


        cursorTransform.position = pos;
    }
}
