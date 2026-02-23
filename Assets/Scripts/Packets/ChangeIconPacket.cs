using Game.Enum;
using Photon.Pun;

public class ChangeIconPacket : RPCPacket
{
    public override PacketType type => PacketType.ChangeIcon;

    public int ActorNumber;
    public int Sprite;

    public ChangeIconPacket(int actorNumber, int sprite)
    {
        ActorNumber = actorNumber;
        Sprite = sprite;
    }

    public override void Send()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Request), RpcTarget.MasterClient, type, new object[] { ActorNumber, Sprite });
        }
        else if (Check())
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Apply), RpcTarget.All, type, new object[] { ActorNumber, Sprite });
        }
    }

    public override bool Check()
    {
        if (!PhotonNetwork.IsMasterClient) return false;

        if (GameManager.Instance.Config.Icons.Count <= Sprite) return false;

        return true;
    }

    public override void Response()
    {
        GameManager.Instance.FindPlayer(ActorNumber).Icon = GameManager.Instance.Config.Icons[Sprite];
        if (ActorNumber == GameManager.Instance.Player.ActorNumber) UIManager.Instance.hud.UpdateIcon();
    }
}