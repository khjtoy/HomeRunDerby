using UnityEngine;

[CreateAssetMenu(fileName = "DefenderInfoData", menuName = "SO/DefenderInfoData")]
public class DefenderInfoData : ScriptableObject
{
    [Header("Catch 가능 범위")]
    [Range(1f, 10f)]
    public float catchDist = 5f;
    [Header("Chase 상태 속도")]
    public float ChaseSpeed = 10f;
}
