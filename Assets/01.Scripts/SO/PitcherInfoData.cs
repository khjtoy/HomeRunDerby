using UnityEngine;

[CreateAssetMenu(fileName = "PitcherInfoData", menuName = "SO/PitcherInfoData")]
public class PitcherInfoData : ScriptableObject
{
    [Header("���� �����ð�")]
    [Range(0.5f, 3f)]
    public float ballArrivalT = 1;
    [Header("���� ���� ��ġ")]
    public float minPosX = 0f;
    public float maxPosX = 0.55f;
    [Header("�� ��Ÿ ����")]
    public float hideBall = 2f;
    [Header("�� �������� ��ġ���� ����")]
    public float timeStep = 0.001f;
    public float maxSimulationTime = 0.034f;
}
