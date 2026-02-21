using Game.Enum;
using NUnit.Framework;

public static class RPCPacketFactory
{
    public static RPCPacket Create(PacketType type, params object[] parameters)
    {
        switch (type)
        {
            case PacketType.TravelSelection:
                return new TravelSelectionPacket(
                    (int)parameters[0],
                    (int)parameters[1]);

            case PacketType.Chat:
                return new ChatPacket(
                    (int)parameters[0],
                    (string)parameters[1]);

            case PacketType.ItemSubmit:
                return new ItemSubmitPacket(
                    (int)parameters[0],
                    (int[])parameters[1]);
            case PacketType.VoteConfirm:
                return new VoteConfirmPacket(
                    (int)parameters[0],
                    (int[])parameters[1]);
        }

        return null;
    }
}

public abstract class RPCPacket
{
    public abstract PacketType type { get; }
    public abstract void Send();
    public abstract bool Check();
    public abstract void Response();
}