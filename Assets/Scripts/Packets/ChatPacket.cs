using Game.Enum;
using Photon.Pun;
using System;

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
        var player = GameManager.Instance.FindPlayer(ActorNumber);

        if (player == null) return;
        if (player.AreaIndex != GameManager.Instance.Player.AreaIndex) return;

        UIManager.Instance.hud.ReceiveChat(player.Icon, ActorNumber == GameManager.Instance.Player.ActorNumber, Text);
    }
}