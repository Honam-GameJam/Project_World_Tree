using Com.MyCompany.MyGame;
using Game.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugingPanel : MonoBehaviour
{
    [SerializeField] Launcher launcher;
    [SerializeField] Debug_PlayerElement _playerPrefab;
    [SerializeField] Transform _playerPrefabParent;

    [Header("Players")]
    int id = 0;
    List<Player> players = new();

    [Header("Interface")]
    public Button AddDummyPlayer;
    public Button DeleteDummyPlayer;

    private void Awake()
    {
        AddDummyPlayer.onClick.AddListener(AddPlayer);
        DeleteDummyPlayer.onClick.AddListener(RemovePlayer);
    }

    private void Start()
    {
        if (launcher != null)
        {

        }
    }

    public void AddPlayer()
    {
        var player = Instantiate(_playerPrefab, _playerPrefabParent);
        var newId = id++;

        player.Init(Random.Range(0, 5), newId);
        players.Add(new Player(newId, $"dummy{newId}"));
    }

    public void RemovePlayer()
    {
        Destroy(_playerPrefabParent.GetChild(_playerPrefabParent.childCount - 1).gameObject);
        id--;
        players.RemoveAt(players.Count-1);
    }
}
