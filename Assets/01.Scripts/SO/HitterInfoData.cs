using UnityEngine;

[CreateAssetMenu(fileName = "HitterInfoData", menuName = "SO/HitterInfoData")]
public class HitterInfoData : ScriptableObject
{
    [Header("밀어치기 / 당겨치기 기준 값")]
    [Range(60.8f, 62f)]
    public float hitStandardValue = 61.5f;
    public float minHitValue = 60.8f;
    public float maxHitValue = 62f;
    [Header("공의 타구 방향 설정 값")]
    [Range(0f, 90f)]
    public float hittingRotY = 60f;
    [Header("타격 기준 값")]
    public float minSwingForce = 1500f;
    public float maxStandardSwingForce = 2500f;
    public float addswingForce = 3000f;
    public float minheight = 3f;
    public float maxStandardHeight = 15f;
    public float addHeight = 10f;
}
