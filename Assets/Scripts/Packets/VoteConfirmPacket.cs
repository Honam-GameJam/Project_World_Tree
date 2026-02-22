using Game.Enum;
using Photon.Pun;

public class VoteConfirmPacket : RPCPacket
{
    public override PacketType type => PacketType.VoteConfirm;

    public int ActorNumber;
    public int[] SelectionArr;

    public VoteConfirmPacket(int actorNumber, int[] selectionArr)
    {
        ActorNumber = actorNumber;
        SelectionArr = selectionArr;
    }

    public override void Send()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Request), RpcTarget.MasterClient, type, new object[] { ActorNumber, SelectionArr });
        }
        else
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Apply), RpcTarget.All, type, new object[] { ActorNumber, SelectionArr });
        }
    }

    public override bool Check()
    {
        if (!PhotonNetwork.IsMasterClient)
            return false;
        var player = GameManager.Instance.FindPlayer(ActorNumber);
        if (!player.IsLeader) return false;
        else player.IsLeader = false;

        return true;
    }

    public override void Response()
    {
        foreach (var player in GameManager.Instance.Players) {
            foreach (int id in SelectionArr) {
                if (player.ActorNumber == id)
                {
                    player.HasShipTicket = true;
                }
            }
        }
    }
}
