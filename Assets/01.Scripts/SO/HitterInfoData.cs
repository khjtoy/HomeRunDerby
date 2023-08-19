using UnityEngine;

[CreateAssetMenu(fileName = "HitterInfoData", menuName = "SO/HitterInfoData")]
public class HitterInfoData : ScriptableObject
{
    [Header("�о�ġ�� / ���ġ�� ���� �߰� ��")]
    [Range(60.8f, 62f)]
    public float hitStandardValue = 61.5f;
    [Header("���� Ÿ�� ���� ���� ��")]
    [Range(0f, 90f)]
    public float hittingRotY = 60f;
}
