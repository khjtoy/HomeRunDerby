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
    private float basePosY2;
    private Vector3 originCursorPos;

    private void Awake()
    {
        cursorTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        basePosY = cursorTransform.localPosition.y;
        basePosY2 = cursorTransform.position.y;
        originCursorPos = cursorTransform.localPosition;

        EventManager.StartListening("RePitching", SetOriginPos);
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
        Vector3 pos = cursorTransform.localPosition;

        if (Define.Pitcher.currentBall != null)
        {
            pos.y = Define.Pitcher.BallPosY;

            if (Define.Pitcher.BallPosY > 1.8f)
            {
                pos.y = Define.Pitcher.BallPosY + 130f;
            }
            else
            {
                pos.y = Define.Pitcher.BallPosY - (Define.Pitcher.PitcherInfo.maxPosY - Define.Pitcher.BallPosY) * 400f;
            }
/*            else if (Define.Pitcher.BallPosY <= 1.4f)
            {
                pos.y = Define.Pitcher.BallPosY - 340f;
            }
            else if (Define.Pitcher.BallPosY <= 1.6f)
            {
                pos.y = Define.Pitcher.BallPosY - 170f;
            }
            else
                pos.y = Define.Pitcher.BallPosY;*/

            /*            if (Define.Pitcher.BallPosY < 1.55f)
                            pos.y = Define.Pitcher.BallPosY - 0.4f;
                        else if (Define.Pitcher.BallPosY > 1.7f)
                            pos.y = Define.Pitcher.BallPosY + 0.1f;
                        else
                            pos.y = Define.Pitcher.BallPosY - 0.2f;*/
        }

        pos.y = Mathf.Clamp(pos.y, minPosY, maxPosY);

        // 기본 크기보다 크면(-180(Base) <)
        if (pos.y >= basePosY)
        {
            float check = -180 - ((pos.y - basePosY) * upOffset);
            bat.localRotation = Quaternion.Euler(check, 0, 0);
            cursorTransform.localRotation = Quaternion.Euler(0f, 0f, -21 + ((pos.y - basePosY) * 0.05f));
        }
        else
        {
            float check = -180 + ((basePosY - pos.y) * downOffset);
            bat.localRotation = Quaternion.Euler(check, 0, 0);
            cursorTransform.localRotation = Quaternion.Euler(0f, 0f, -21 - ((basePosY - pos.y) * 0.03f));
        }


         // Debug.Log(pos);
        cursorTransform.localPosition = pos;
    }

    private void CursorMove()
    {
        // float v = Input.GetAxisRaw("Vertical");

        float v = joystick.Vertical;

        Vector3 pos = cursorTransform.localPosition;

        pos.y += v * speed * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, minPosY, maxPosY);

        // 기본 크기보다 크면(-180(Base) <)
        if (pos.y >= basePosY)
        {
            float check = -180 - ((pos.y - basePosY) * upOffset);
            bat.localRotation = Quaternion.Euler(check, 0, 0);
            cursorTransform.localRotation = Quaternion.Euler(0f, 0f, -21 + ((pos.y - basePosY) * 0.05f));
        }
        else
        {
            float check = -180 + ((basePosY - pos.y) * downOffset);
            bat.localRotation = Quaternion.Euler(check, 0, 0);
            cursorTransform.localRotation = Quaternion.Euler(0f, 0f, -21 - ((basePosY - pos.y) * 0.03f));
        }


        cursorTransform.localPosition = pos;
    }

    private void SetOriginPos(EventParam eventParam)
    {
        cursorTransform.localPosition = originCursorPos;
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("RePitching", SetOriginPos);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("RePitching", SetOriginPos);
    }
}
