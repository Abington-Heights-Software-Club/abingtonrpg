using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//testing for File
public static class CurrentPartyData
{
    public static int partySize = 4;
    public static int currentPartySize = 0;
    public static int inventorySize = 10;
    public static int energyMax = 100;
    public static int goodQuestionTokens = 0;
    //Array of PartyMember objects that store the user's current party
    public static PartyMember[] party = new PartyMember[partySize];
    //Array of custom made Tuples that holds the item (type: ConsumableData) and itemAmount (type: int)
    public static InventoryItem<ConsumableData, int>[] inventory = new InventoryItem<ConsumableData, int>[inventorySize];
    
    //Takes int position and id (type: int) and adds a PartyMember object with the id into party array from 0-partySize
    //Will fill up first available empty space. If there are no spaces it will swap the partyMember at int position with new one
    public static void addMember(string id, int position = -1)
    {
        
        if(currentPartySize < partySize)
        {
            //Debug.Log("First empty party " + currentPartySize);
            party[currentPartySize] = new PartyMember(id);
            currentPartySize++;
        } 
        else
        {
            party[position] = new PartyMember(id);
        }
    }

    //Takes newItem (type: string) add adds to inventory. If one already exists it increases itemAmount by 1
    public static void addItem(string newItemID)
    {
        int firstEmpty = Array.FindIndex(inventory, i => i == null);
        ConsumableData newItemData = new ConsumableData(newItemID);
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
        public int currentMaxHealth { get; private set; }
        public int currentHealth { get; set; }
        public int currentLevel { get; private set;  }
        public int currentXP { get; private set; }
        public int currentXPCap { get; private set; }
        public int currentLowDamage { get; private set; }
        public int currentHighDamage { get; private set; }
        public WeaponData equippedWeapon { get; private set; }
        public WearableData equippedArmor { get; private set; }
        public int currentEnergy { get; private set; }
        public PartyMember(string id)
        {
            playerData = new PlayableCharacterData(id);
            currentMaxHealth = playerData.base_health;
            currentHealth = currentMaxHealth;
            currentXP = 0;
            currentLevel = 0;
            currentXPCap = playerData.base_xp_cap;
            currentLowDamage = playerData.base_low_damage;
            currentHighDamage = playerData.base_high_damage;
            equippedArmor = null;
            equippedWeapon = null;
            currentEnergy = energyMax;
        }
        public void addXP(int addedXP)
        {
            currentXP += addedXP;
            while(currentXP >= currentXPCap)
            {
                currentXP -= currentXPCap;
                levelUp();
            }
        }
        private void levelUp()
        {
            currentMaxHealth += playerData.levelUp_health;
            currentHealth +=playerData.levelUp_health;
            currentLowDamage += playerData.levelUp_damage;
            currentHighDamage += playerData.levelUp_damage;
            currentXPCap += playerData.levelUp_xp_cap;
            currentLevel++;
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

