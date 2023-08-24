using UnityEngine;

[CreateAssetMenu(fileName = "DefenderInfoData", menuName = "SO/DefenderInfoData")]
public class DefenderInfoData : ScriptableObject
{
    [Header("Catch ���� ����")]
    [Range(1f, 10f)]
    public float catchDist = 5f;
    [Header("Chase ���� �ӵ�")]
    public float ChaseSpeed = 10f;
}
