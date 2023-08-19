using UnityEngine;

[CreateAssetMenu(fileName = "HitterInfoData", menuName = "SO/HitterInfoData")]
public class HitterInfoData : ScriptableObject
{
    [Header("밀어치기 / 당겨치기 기준 중간 값")]
    [Range(60.8f, 62f)]
    public float hitStandardValue = 61.5f;
    [Header("공의 타구 방향 설정 값")]
    [Range(0f, 90f)]
    public float hittingRotY = 60f;
}
