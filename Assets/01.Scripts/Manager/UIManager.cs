using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
    
public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI homerunText;
    [SerializeField]
    private TextMeshProUGUI outText;
    [SerializeField]
    private Image StrikeZone;
    [Header("Result Panel")]
    [SerializeField]
    private Image resultPanel;
    [SerializeField]
    private TextMeshProUGUI rankText;
    [SerializeField]
    private TextMeshProUGUI resultHRText;

    private int m_HRCount = 0;
    private int m_outCount = 0;

    protected override void Init()
    {
       // Destroyed
    }

    private void Start()
    {
        resultPanel.gameObject.SetActive(false);
    }

    public void AddHRCount()
    {
        homerunText.text = $"HomeRun: {++m_HRCount}";
    }

    public int AddOutCount()
    {
        outText.text = $"Out: {++m_outCount} / 10";

        return m_outCount;
    }

    public void ActiveStrikeZone(bool setActive)
    {
        StrikeZone.gameObject.SetActive(setActive);
    }

    public void Result()
    {
        resultPanel.gameObject.SetActive(true);
        rankText.text = "A";
        resultHRText.text = homerunText.text;
    }

    public void RestartGame()
    {
        PoolManager.Instance.AllDespawn((int)Define.PooledObject.Ball);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
