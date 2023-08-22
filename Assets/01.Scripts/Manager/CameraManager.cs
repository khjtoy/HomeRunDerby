using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField]
    private CinemachineVirtualCamera mainCam;
    [SerializeField]
    private CinemachineVirtualCamera trackBallCam;

    protected override void Init()
    { 
        // DontDestroyOnLoad Cancel
    }

    public void FollowBall(Transform ballTrans)
    {
        trackBallCam.LookAt = ballTrans;
        trackBallCam.Follow = ballTrans;
        SwitchBallCam(true);
    }

    public void SwitchBallCam(bool SetTrack)
    {
        if(SetTrack)
        {
            trackBallCam.Priority = 1;
            mainCam.Priority = 0;
        }
        else
        {
            mainCam.Priority = 1;
            trackBallCam.Priority = 0;
        }
    }
}
