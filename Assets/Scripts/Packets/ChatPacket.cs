using Game.Enum;
using Photon.Pun;

public class ChatPacket : RPCPacket
{
    public override PacketType type => PacketType.Chat;

    public int ActorNumber;
    public string Text;

    public ChatPacket(int actorNumber, string text)
    {
        ActorNumber = actorNumber;
        Text = text;
    }

    public override void Send()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Request), RpcTarget.MasterClient, type, new object[] { ActorNumber, Text });
        }
        else if (Check())
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Apply), RpcTarget.All, type, new object[] { ActorNumber, Text });
        }
    }

    public override bool Check()
    {
        if (!PhotonNetwork.IsMasterClient) return false;

        return true;
    }

    public override void Response()
    {
        GameManager.Instance.SendChat(ActorNumber, Text);
    }
}