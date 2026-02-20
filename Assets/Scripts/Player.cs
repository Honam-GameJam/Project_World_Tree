namespace Game.Data
{
    public class Player
    {
        public readonly int ActorNumber;
        public string Name;
        public bool hasSelected;
        public int AreaIndex;
        public int Money;
        public int[] Inventory;

        public Player(int actorNumber, string name)
        {
            ActorNumber = actorNumber;
            Name = name;
            Inventory = new int[6];
        }
    }
}