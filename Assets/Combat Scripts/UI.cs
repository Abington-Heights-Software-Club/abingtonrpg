using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    //All of the players UI elements
    public Slider[] playerHpSliders = new Slider[4];
    public Text[] playerNameTexts = new Text[4];
    public Text[] playerLevelTexts = new Text[4];

    //All of the enemies UI elements
    public Slider[] enemyHpSliders = new Slider[4];
    public Text[] enemyNameTexts = new Text[4];
    
    //UI text where interaction with user is held
    public Text dialogue;

    //sets all standard player info
    public void SetPlayerHUD()
    {
        int i = 0;
        foreach(CurrentPartyData.PartyMember entity in CurrentPartyData.party)
        {
            if(entity == null)
            {
                playerHpSliders[i].gameObject.SetActive(false);
                playerNameTexts[i].gameObject.SetActive(false);
                playerLevelTexts[i].gameObject.SetActive(false);
            }
            else
            {
                playerNameTexts[i].text = entity.playerData.name;
                playerLevelTexts[i].text = "Lvl. " + entity.currentLevel;
                playerHpSliders[i].maxValue = entity.currentMaxHealth;
                playerHpSliders[i].value = entity.currentHealth;
            }
            i++;
        }
        
    }
    //sets all standard enemy info
    public void SetEnemyHUD(){
        int i = 0;
        foreach (CombatEnemyData.CommonCombatEnemy entity in CombatEnemyData.commonCombatEnemyParty)
        {
            if (entity == null)
            {
                enemyHpSliders[i].gameObject.SetActive(false);
                enemyNameTexts[i].gameObject.SetActive(false);
            }
            else
            {
                enemyNameTexts[i].text = entity.combatEnemyData.name;
                enemyHpSliders[i].maxValue = entity.combatEnemyData.health;
                enemyHpSliders[i].value = entity.combatEnemyData.health;
            }
            i++;
        }
    }
    //health right now is the only thing that changes in battle so it has it's own method
    public void RefreshPlayerHp(){
        int i = 0;
        foreach (CurrentPartyData.PartyMember entity in CurrentPartyData.party)
        {
            if (entity != null)
            {
                playerHpSliders[i].value = entity.currentHealth;
            }
            i++;
        }
    }
    public void RefreshEnemyHp()
    {
        int i = 0;
        foreach (CombatEnemyData.CommonCombatEnemy entity in CombatEnemyData.commonCombatEnemyParty)
        {
            if (entity != null)
            {
                enemyHpSliders[i].value = entity.currentHealth;
            }
            i++;
        }
    }
   public void SetText(string message)
    {
        dialogue.text = message;
    }

    
}
