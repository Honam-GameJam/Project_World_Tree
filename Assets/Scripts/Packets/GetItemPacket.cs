using Game.Enum;
using Photon.Pun;

public class GetItemPacket : RPCPacket
{
    public override PacketType type => PacketType.GetItem;

    public int ActorNumber;
    public int Index;

    public GetItemPacket(int actorNumber, int index)
    {
        ActorNumber = actorNumber;
        Index = index;
    }

    public override void Send()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Request), RpcTarget.MasterClient, type, new object[] { ActorNumber, Index });
        }
        else if (Check())
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Apply), RpcTarget.All, type, new object[] { ActorNumber, Index });
        }
    }

    public override bool Check()
    {
        if (!PhotonNetwork.IsMasterClient) return false;



        return true;
    }

    public override void Response()
    {

    }
}