using UnityEngine;

[CreateAssetMenu(fileName = "PitcherInfoData", menuName = "SO/PitcherInfoData")]
public class PitcherInfoData : ScriptableObject
{
    [Header("공의 도착시간")]
    [Range(0.5f, 3f)]
    public float ballArrivalT = 1;
    [Header("공의 랜덤 위치")]
    public float minPosX = 0f;
    public float maxPosX = 0.55f;
    [Header("공 기타 설정")]
    public float hideBall = 2f;
    [Header("공 낙하지점 위치예측 변수")]
    public float timeStep = 0.001f;
    public float maxSimulationTime = 0.034f;
}
