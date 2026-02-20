using Game.Enum;
using Photon.Pun;
using UnityEngine;

public class GameTrigger : MonoBehaviour
{
    private void Awake()
    {
        var ui = UIManager.Instance;

        GameManager.Instance.InitPlayers();
        GameManager.Instance.SetPhase(Phase.TravelSelection);
    }
}