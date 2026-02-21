using UnityEngine;

namespace Game.Data
{
    public class Player
    {
        public readonly int ActorNumber;
        public string Name;
        public Sprite Icon;
        public bool hasSelected;
        public int AreaIndex;
        public int Money;
        public bool IsLeader;
        public int[] Inventory;
        public int[] Ship;
        public bool HasShipTicket;
        public bool IsActionFinished;

        public Player(int actorNumber, string name)
        {
            ActorNumber = actorNumber;
            Name = name;
            Inventory = new int[6] { -1, -1, -1, -1, -1, -1 };
            Ship = new int[3] { -1, -1, -1 };
        }
    }
}