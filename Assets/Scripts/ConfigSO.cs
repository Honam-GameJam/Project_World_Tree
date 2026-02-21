using UnityEngine;

[CreateAssetMenu(fileName = "ConfigSO", menuName = "Scriptable Objects/ConfigSO")]
public class ConfigSO : ScriptableObject
{
    [field: SerializeField] public int DefaultMoney { get; private set; } = 10;
    [field: SerializeField] public float DefaultTravelSelectionTime { get; private set; } = 30;
    [field: SerializeField] public float DefaultTravelTime { get; private set; } = 30;
    [field: SerializeField] public int Round { get; private set; } = 5;
    [field: SerializeField] public int TravelCycle { get; private set; } = 2;
    [field: SerializeField] public int MaxPlayer { get; private set; } = 6;
    [field: SerializeField] public int VotedPlayer { get; private set; } = 4;
}
