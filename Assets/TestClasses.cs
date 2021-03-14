using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClasses : MonoBehaviour
{
    void Start()
    {
        //CommonEnemy enemy = new CommonEnemy("chromebook_n22");
        //Debug.Log(enemy.name);
        //QuestData quest = new QuestData("getting_started");
        //Debug.Log(quest.xp_gained);
        //Weapon poison_vial = new Weapon("test_tube");
        //Debug.Log(poison_vial.weapon_name);
        //NPCData npc = new NPCData("michael");
        //Debug.Log(npc.dialogue);
        //PlayableCharacterData character = new PlayableCharacterData("vannan");
        //Debug.Log(character.levelUp_xp_cap);
        //BossData character = new BossData("stroyan");
        //Debug.Log(character.health);
        CurrentPartyData.addMember("sharrow");
        CurrentPartyData.addMember("vannan");
        CurrentPartyData.addMember("sharrow");
        foreach(CurrentPartyData.PartyMember member in CurrentPartyData.party)
        {
            if(member == null)
            {
                Debug.Log("null");
            }
            else
            {
                Debug.Log(member.playerData.name);
            }
        }
        CurrentPartyData.party[0].addXP(210);
        CurrentPartyData.party[0].currentHealth -= 10;
        //Debug.Log("Sharrow Health: " + CurrentPartyData.party[0].currentHealth + " Level: " + CurrentPartyData.party[0].currentLevel + " XP: " + CurrentPartyData.party[0].currentXP + " Low Damage: " + CurrentPartyData.party[0].currentLowDamage);
        CombatEnemyData.setCombatEnemy(false, new string[] { "chromebook_n22", "chromebook_n21" });
        Debug.Log(CombatEnemyData.commonCombatEnemyParty[0].combatEnemyData.name); 
        CurrentPartyData.addItem("heal");
        CurrentPartyData.addItem("heal");
        CurrentPartyData.addItem("wither");
        Debug.Log(CurrentPartyData.inventory[0].item.consumable_name + " " + CurrentPartyData.inventory[0].itemAmount + " " + CurrentPartyData.inventory[1].item.consumable_name + " " + CurrentPartyData.inventory[1].itemAmount);
        if(CurrentPartyData.inventory[2] == null)
        {
            Debug.Log("Nothing!");
        }
    }
}
