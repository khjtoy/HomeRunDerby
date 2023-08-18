using UnityEngine;

[CreateAssetMenu(fileName = "PitcherInfoData", menuName = "SO/PitcherInfoData")]
public class PitcherInfoData : ScriptableObject
{
    [Range(0.5f, 3f)]
    public float ballArrivalT = 1;
}
