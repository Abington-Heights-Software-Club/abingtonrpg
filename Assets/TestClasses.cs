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
        //Debug.Log(character.name);
        BossData character = new BossData("stroyan");
        Debug.Log(character.low_damage);
    }
}
