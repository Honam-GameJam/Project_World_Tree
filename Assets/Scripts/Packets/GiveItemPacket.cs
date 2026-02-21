using Game.Enum;
using Photon.Pun;
using System;

public class GiveItemPacket : RPCPacket
{
    public override PacketType type => PacketType.GiveItem;

    public int ActorNumber;
    public int[] Items;

    public GiveItemPacket(int actorNumber, int[] items)
    {
        ActorNumber = actorNumber;
        Items = items;
    }

    public override void Send()
    {
    }

    public override bool Check()
    {
        return false;
    }

    public override void Response()
    {
        var player = GameManager.Instance.FindPlayer(ActorNumber);

        if (player == null) return;

        foreach (var item in Items)
        {
            for (int i = 0; i < player.Inventory.Length; i++)
            {
                if (player.Inventory[i] == -1)
                {
                    player.Inventory[i] = item;
                    break;
                }
            }
        }

        if (GameManager.Instance.Player.ActorNumber == ActorNumber)
            UIManager.Instance.hud.UpdateInventory();
    }
}