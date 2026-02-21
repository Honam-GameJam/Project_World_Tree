namespace Game.Enum
{
    public enum Phase
    {
        InLobby,
        TravelSelection,
        Travel,
        GoHome,
        Vote,
        VoteResult,
        Feed,    
        CheckWorldTree,
        GameResult,
    }

    public enum PacketType
    {
        TravelSelection,
        ChangeMoney,
        GetItem,
        ItemSubmit,
        Chat,
    }
}