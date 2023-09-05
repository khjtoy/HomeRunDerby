using UnityEngine;

[CreateAssetMenu(fileName = "DefenderInfoData", menuName = "SO/DefenderInfoData")]
public class DefenderInfoData : ScriptableObject
{
    [Header("Catch 가능 범위")]
    public float catchDist = 1f;
    public float flyCatchDist = 30f;
    [Header("Ground 캐치 설정 범위")]
    public float speedPercent = 0.08f;
    public float groundSpeed = 2f;
    [Header("fly 캐치 설정 범위")]
    public float offsetX = 4f;
    public float flySpeed = 15f;
    [Header("Chase 상태 속도")]
    public float chaseSpeed = 10f;
    [Header("시야각 설정")]
    public float viewAngle;    //시야각
    public float viewDistance; //시야거리
}
