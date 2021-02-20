using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrentPartyData
{
    public static int partySize = 4;
    public static int inventorySize = 10;
    public static PartyMember[] party = new PartyMember[partySize];
    public static Tuple<ConsumableData, int>[] inventory = new Tuple<ConsumableData, int>[inventorySize];
    
    //Takes position and id and adds a PartyMember object with the id into party array from 0-partySize
    public static void addMember(int position, string id)
    {
        party[position] = new PartyMember(id);
    }
    public class PartyMember {
        public PlayableCharacterData playerData { get; private set; }
        public int currentHealth { get; set; }
        public int currentXP { get; private set; }
        public PartyMember(string id)
        {
            playerData = new PlayableCharacterData(id);
            currentHealth = playerData.health;
            currentXP = 0;
        }
    }
    public class Tuple<T, U>
    {
        public T Item1 { get; private set; }
        public U Item2 { get; private set; }

        public Tuple(T item1, U item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }

    public static class Tuple
    {
        public static Tuple<T, U> Create<T, U>(T item1, U item2)
        {
            return new Tuple<T, U>(item1, item2);
        }
    }
}

