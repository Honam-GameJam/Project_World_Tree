using Game.Enum;
using Photon.Pun;
using UnityEngine;

public class AsyncPhasePacket : RPCPacket
{
    public override PacketType type => PacketType.AsyncPhase;

    public int ActorNumber;

    public AsyncPhasePacket(int actorNumber)
    {
        ActorNumber = actorNumber;
    }

    public override void Send()
    {
        UIManager.Instance.cover.gameObject.SetActive(true);

        if (!PhotonNetwork.IsMasterClient)
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Request), RpcTarget.MasterClient, type, new object[] { ActorNumber });
        }
        else if (Check())
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Apply), RpcTarget.All, type, new object[] { ActorNumber });
        }
    }

    public override bool Check()
    {
        if (!PhotonNetwork.IsMasterClient) return false;

        GameManager.Instance.FindPlayer(ActorNumber).IsActionFinished = true;
        Debug.Log(ActorNumber);

        foreach (var player in GameManager.Instance.Players)
        {
            if (player.IsActionFinished == false) return false;
        }

        foreach (var player in GameManager.Instance.Players)
        {
            player.IsActionFinished = false;
        }

        return true;
    }

    public override void Response()
    {
        var phase = GameManager.Instance.Phase;
        Phase nextPhase = phase switch {
            Phase.InLobby => Phase.TravelSelection,
            Phase.TravelSelection => Phase.Travel,
            Phase.Travel => Phase.GoHome,
            Phase.GoHome => GameManager.Instance.MustTravel ? Phase.TravelSelection : Phase.Vote,
            Phase.Vote => Phase.VoteResult,
            Phase.VoteResult => Phase.Feed,
            Phase.Feed => Phase.RoundResult,
            _ => Phase.InLobby,
        };

        GameManager.Instance.SetPhase(nextPhase);

        UIManager.Instance.cover.gameObject.SetActive(false);
    }
}