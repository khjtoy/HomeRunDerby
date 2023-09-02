using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public enum ResultState { StrikeOut, Ground, Foul, HR };
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
    [SerializeField]
    private TextMeshProUGUI judgmentText;

    [Header("Result Color")]
    [SerializeField]
    private Color outColor;
    [SerializeField]
    private Color foulColor;
    [SerializeField]
    private Color homeRunColor;
    [SerializeField]
    private Color groundBallColor;

    [Header("Meet")]
    [SerializeField]
    private Image cursorImage;
    [SerializeField]
    private Image ballImage;

    [Header("Mode")]
    [SerializeField]
    private Image modeImage;
    [SerializeField]
    private TextMeshProUGUI modeText;

    private int m_HRCount = 0;
    private int m_outCount = 0;

    protected override void Init()
    {
       // Destroyed
    }

    private void Start()
    {
        resultPanel.gameObject.SetActive(false);
        judgmentText.gameObject.SetActive(false);
        ballImage.gameObject.SetActive(false);
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

    public void DisableJudmentText()
    {
        judgmentText.gameObject.SetActive(false);
    }

    public void Result()
    {
        resultPanel.gameObject.SetActive(true);
        rankText.text = "A";
        resultHRText.text = homerunText.text;
    }

    public void Judgment(ResultState result)
    {
        switch (result)
        {
            case ResultState.StrikeOut:
                judgmentText.text = "Strike";
                judgmentText.color = outColor;
                judgmentText.DOFade(1, 0.2f);
                break;
            case ResultState.Ground:
                judgmentText.text = "Ground Ball";
                judgmentText.color = groundBallColor;
                judgmentText.DOFade(1, 0.4f);
                break;
            case ResultState.Foul:
                judgmentText.text = "Foul";
                judgmentText.color = foulColor;
                judgmentText.DOFade(1, 0.4f);
                break;
            case ResultState.HR:
                judgmentText.text = "HomeRun!!";
                judgmentText.color = homeRunColor;
                judgmentText.transform.rotation = Quaternion.Euler(0f, -80f, 0f);
                judgmentText.transform.DORotate(Vector3.zero, 0.6f);
                break;
        }
        judgmentText.gameObject.SetActive(true);
    }

    public void ActiveCursor(bool visible)
    {
        cursorImage.gameObject.SetActive(visible);
    }

    public void ActiveBallCursor(bool visible, Vector3 pos = new Vector3())
    {
        Vector3 setPos = pos.SetZ(ballImage.transform.position.z);
        if (visible)
            ballImage.transform.position = setPos;

        ballImage.gameObject.SetActive(visible);
    }

    public void ActiveMode(bool visible)
    {
        modeImage.gameObject.SetActive(visible);
    }

    public void ChangeModeUI()
    {
        GameManager.Instance.ChangeAutoMode();

        if(GameManager.Instance.AutoMode)
        {
            modeText.text = "Auto";
        }
        else
        {
            modeText.text = "Manual";
        }
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
