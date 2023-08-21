using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultManager : MonoSingleton<ResultManager>
{
    [SerializeField]
    private TextMeshProUGUI homerunText;
    [SerializeField]
    private TextMeshProUGUI outText;

    private int m_HRCount = 0;
    private int m_outCount = 0;

    protected override void Init()
    {
       // Destroyed
    }

    public void AddHRCount()
    {
        homerunText.text = $"HomeRun: {++m_HRCount}";
    }

    public void AddOutCount()
    {
        outText.text = $"Out: {++m_outCount} / 10";
    }
}
