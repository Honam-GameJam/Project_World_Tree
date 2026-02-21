namespace Game.Enum
{
    public enum Phase
    {
        InLobby,
        TravelSelection,
        Travel,
        Vote,
        Feed,    
        CheckWorldTree,
        GameResult,
    }

    public enum PacketType
    {
        TravelSelection,
        ChangeMoney,
        GetItem,
        Chat,
    }
}