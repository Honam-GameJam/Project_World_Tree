using Game.Enum;
using Photon.Pun;

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

        foreach (var player in GameManager.Instance.Players)
        {
            if (player.IsActionFinished == false) return false;
        }

        return true;
    }

    public override void Response()
    {
        GameManager.Instance.Player.IsActionFinished = false;

        var phase = GameManager.Instance.Phase;
        Phase nextPhase = phase switch {
            Phase.InLobby => Phase.TravelSelection,
            Phase.TravelSelection => Phase.Travel,
            Phase.Travel => Phase.GoHome,
            Phase.GoHome => GameManager.Instance._mustTravel ? Phase.TravelSelection : Phase.Vote,
            Phase.Vote => Phase.VoteResult,
            Phase.VoteResult => Phase.Feed,
            Phase.Feed => Phase.GameResult,
            _ => Phase.InLobby,
        };

        GameManager.Instance.SetPhase(nextPhase);
    }
}