using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField]
    private CinemachineVirtualCamera mainCam;

    private Vector3 originPos;
    private Quaternion originRot;

    protected override void Init()
    {
        // Don't Destoryed
    }

    private void Start()
    {
        originPos = mainCam.transform.position;
        originRot = mainCam.transform.rotation;
    }

    public void FollowBall(Transform ball)
    {
        mainCam.Follow = ball;
        mainCam.LookAt = ball;
    }

    public void SetOriginCam()
    {
        mainCam.Follow = null;
        mainCam.LookAt = null;

        mainCam.transform.position = originPos;
        mainCam.transform.rotation = originRot;
    }
}
