using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public bool AutoMode { get; private set; } = true;

    protected override void Awake()
    {
        base.Awake();
       //  Application.targetFrameRate = 60;
    }

    public void ChangeAutoMode()
    {
        AutoMode = !AutoMode;
    }
}
