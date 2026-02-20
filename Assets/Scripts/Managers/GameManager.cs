using Game.Data;
using Game.Enum;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _canStartGame;
    public Phase Phase { get; private set; }

    private Dictionary<(Phase, bool), Action> _phaseEvents = new();

    //Master Client
    private Dictionary<int, Player> _players = new();

    int _actorNumber;

    public void InitPlayers()
    {
        foreach(var p in PhotonNetwork.CurrentRoom.Players.Values)
        {
            var player = new Player(p.ActorNumber, p.NickName);
            _players[p.ActorNumber] = player;

            AddListener(Phase.TravelSelection, true, () => player.hasSelected = false);
        }

        _actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
    }

    public void Ready()
    {

    }

    public void GameStart()
    {
        if (!_canStartGame) Debug.Log("게임을 시작할 수 없습니다.");

        SetPhase(Phase.TravelSelection);
    }

    public void ClickArea(int index)
    {
        RPCPacketFactory.Create(PacketType.TravelSelection, _actorNumber, index).Send();
    }

    public void ClickArea(int actorNumber, int index)
    {
        if (!_players.TryGetValue(actorNumber, out Player player)) return;

        player.AreaIndex = index;
        player.hasSelected = true;

        CheckAllPlayersSelected();
    }

    private void CheckAllPlayersSelected()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        bool selected = true;
        foreach(var p in _players.Values)
        {
            selected &= p.hasSelected;
        }

        if (selected) { } //바로 시작?
    }

    public void SetPhase(Phase phase)
    {
        if (_phaseEvents.TryGetValue((Phase, false), out Action entry))
        {
            entry.Invoke();
        }

        Phase = phase;

        if (_phaseEvents.TryGetValue((phase, true), out entry))
        {
            entry.Invoke();
        }
    }

    public void AddListener(Phase phase, bool onEntered, Action action)
    {
        if (_phaseEvents.TryGetValue((phase, onEntered), out Action entry))
        {
            entry += action;
        }
        else
        {
            entry = action;
            _phaseEvents.Add((phase, onEntered), entry);
        }
    }

    public void RemoveListener(Phase phase, bool onEntered, Action action)
    {
        if (_phaseEvents.TryGetValue((phase, onEntered), out Action entry))
        {
            entry -= action;
        }
    }
}