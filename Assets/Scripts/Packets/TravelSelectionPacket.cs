using Game.Enum;
using Photon.Pun;

public class TravelSelectionPacket : RPCPacket
{
    public override PacketType type => PacketType.TravelSelection;

    public int ActorNumber;
    public int SelectionIndex;

    public TravelSelectionPacket(int actorNumber, int selectionIndex)
    {
        ActorNumber = actorNumber;
        SelectionIndex = selectionIndex;
    }

    public override void Send()
    {
        RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Request), RpcTarget.MasterClient, type, new object[] { ActorNumber, SelectionIndex});
    }

    public override bool Check()
    {
        if (!PhotonNetwork.IsMasterClient) return false;

        return true;
    }

    public override void Response()
    {
        GameManager.Instance.ClickArea(ActorNumber, SelectionIndex);
    }
}