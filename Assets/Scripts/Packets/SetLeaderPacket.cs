using Game.Enum;
using Photon.Pun;

public class SetLeaderPacket : RPCPacket
{
    public override PacketType type => PacketType.SetLeader;

    public int ActorNumber;

    public SetLeaderPacket(int actorNumber)
    {
        ActorNumber = actorNumber;
    }

    public override void Send()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        
        if (Check())
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Apply), RpcTarget.All, type, new object[] { ActorNumber });
        }
    }

    public override bool Check()
    {
        if (!PhotonNetwork.IsMasterClient)
            return false;

        return true;
    }

    public override void Response()
    {
        GameManager.Instance.FindPlayer(ActorNumber).IsLeader = true;
    }
}