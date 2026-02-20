using Game.Enum;
using UnityEngine;

public class GameTrigger : MonoBehaviour
{
    private void Awake()
    {
        var ui = UIManager.Instance;

        GameManager.Instance.SetPhase(Phase.TravelSelection);
    }
}