using Game.Data;
using Game.Enum;
using Photon.Pun;
using System;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public Phase Phase { get; private set; }

    private Dictionary<(Phase, bool), Action> _phaseEvents = new();

    //Master Client
    private Dictionary<int, Player> _players = new();

    public IReadOnlyCollection<Player> Players => _players.Values;
    public Player Player => _players[_actorNumber];
    public Player FindPlayer(int actorNumber) => _players.ContainsKey(actorNumber) ? _players[actorNumber] : null;

    private int _actorNumber;

    public int Round { get; private set; }
    public ConfigSO config;

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

    public void DeliverSelectionArray(int[] selectionArr) => RPCPacketFactory.Create(PacketType.VoteConfirm, _actorNumber, selectionArr).Send();

    public void DeliverSelectionArray(int actorNumber, int[] selectionArr)
    {
        if (!_players.TryGetValue(actorNumber, out var player))
            return;
        
        //has ship ticket 구현.
    }
    public void ClickArea(int index) => RPCPacketFactory.Create(PacketType.TravelSelection, _actorNumber, index).Send();
    public void SendChat(string chat) => RPCPacketFactory.Create(PacketType.Chat, _actorNumber, chat).Send();
    public void SubmitItem() => RPCPacketFactory.Create(PacketType.ItemSubmit, _actorNumber, Player.Ship).Send();
    public void SelectOption(int index) => RPCPacketFactory.Create(PacketType.ItemSubmit, _actorNumber, index).Send();

    public void ClickItem(int index, bool isInventory)
    {
        if (isInventory)
        {
            var itemId = Player.Inventory[index];
            Player.Inventory[index] = -1;

            for (int i = 0; i < Player.Ship.Length; i++)
            {
                if (Player.Ship[i] == -1)
                {
                    Player.Ship[i] = itemId;
                    break;
                }
            }
        }
        else
        {
            var itemId = Player.Ship[index];
            Player.Ship[index] = -1;

            for (int i = 0; i < Player.Inventory.Length; i++)
            {
                if (Player.Inventory[i] == -1)
                {
                    Player.Inventory[i] = itemId;
                    break;
                }
            }
        }

        UIManager.Instance.hud.UpdateInventory();
    }


    public void CheckAllPlayersSelected()
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