using Game.Data;
using Game.Enum;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Phase Phase { get; private set; }

    private Dictionary<(Phase, bool), Action> _phaseEvents = new();

    //Master Client
    private Dictionary<int, Player> _players = new();

    public IReadOnlyCollection<Player> Players => _players.Values;
    public Player Player => _players[_actorNumber];

    private int _actorNumber;

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

    public void ClickArea(int index) => RPCPacketFactory.Create(PacketType.TravelSelection, _actorNumber, index).Send();

    public void ClickArea(int actorNumber, int index)
    {
        if (!_players.TryGetValue(actorNumber, out Player player)) return;

        player.AreaIndex = index;
        player.hasSelected = true;

        CheckAllPlayersSelected();
    }

    public void SendChat(string chat) => RPCPacketFactory.Create(PacketType.Chat, _actorNumber, chat).Send();

    public void SendChat(int actorNumber, string chat)
    {
        if (_players.TryGetValue(actorNumber, out var player)) {
            if (player.AreaIndex != Player.AreaIndex) return;

            // No...
            UIManager.Instance.HUD.ReceiveChat(player.Icon, actorNumber == _actorNumber, chat);
        }
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