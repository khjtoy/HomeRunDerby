using UnityEngine;

[CreateAssetMenu(fileName = "DefenderInfoData", menuName = "SO/DefenderInfoData")]
public class DefenderInfoData : ScriptableObject
{
    [Header("Catch ���� ����")]
    public float catchDist = 1f;
    public float flyCatchDist = 30f;
    [Header("Ground ĳġ ���� ����")]
    public float speedPercent = 0.08f;
    public float groundSpeed = 2f;
    [Header("fly ĳġ ���� ����")]
    public float offsetX = 4f;
    public float flySpeed = 15f;
    [Header("Chase ���� �ӵ�")]
    public float chaseSpeed = 10f;
    [Header("�þ߰� ����")]
    public float viewAngle;    //�þ߰�
    public float viewDistance; //�þ߰Ÿ�
}
