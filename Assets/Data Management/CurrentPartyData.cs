using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrentPartyData
{
    public static int partySize = 4;
    public static int inventorySize = 10;
    //Array of PartyMember objects that store the user's current party
    public static PartyMember[] party = new PartyMember[partySize];
    //Array of custom made Tuples that holds the item (type: ConsumableData) and itemAmount (type: int)
    public static InventoryItem<ConsumableData, int>[] inventory = new InventoryItem<ConsumableData, int>[inventorySize];
    
    //Takes int position and id (type: int) and adds a PartyMember object with the id into party array from 0-partySize
    public static void addMember(int position, string id)
    {
        party[position] = new PartyMember(id);
    }

    //Takes newItem (type: string) add adds to inventory. If one already exists it increases itemAmount by 1
    public static void addItem(string newItem)
    {
        int firstEmpty = Array.FindIndex(inventory, i => i == null);
        ConsumableData newItemData = new ConsumableData(newItem);
        for (int i = 0; i < firstEmpty; i++)
        {
            if(newItemData.consumable_name.Equals(inventory[i].item.consumable_name))
            {
                inventory[i].itemAmount++;
                return;
            }
        }
        inventory[firstEmpty] = new InventoryItem<ConsumableData, int>(newItemData, 1);
    }

    //Custom class to store all database data and current data of the user's party members
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

    //Custom Tuple class which stores item (type: ConsumableData) and itemAmount (type: int)
    public class InventoryItem<ConsumableData, @int>
    {
        public ConsumableData item { get; private set; }
        public @int itemAmount { get; set; }

        public InventoryItem(ConsumableData item1, @int item2)
        {
            item = item1;
            itemAmount = item2;
        }
    }

    //Static class which creates InventoryItem objects
    public static class InventoryItem
    {
        public static InventoryItem<ConsumableData, @int> Create<ConsumableData, @int>(ConsumableData item, @int itemAmount)
        {
            return new InventoryItem<ConsumableData, @int>(item, itemAmount);
        }
    }
}

