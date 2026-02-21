using UnityEngine;

[CreateAssetMenu(fileName = "ConfigSO", menuName = "Scriptable Objects/ConfigSO")]
public class ConfigSO : ScriptableObject
{
    [field: SerializeField] public float DefaultTravelSelectionTime { get; private set; } = 30;
    [field: SerializeField] public float DefaultTravelTime { get; private set; } = 30;
    [field: SerializeField] public float Round { get; private set; } = 5;
}
