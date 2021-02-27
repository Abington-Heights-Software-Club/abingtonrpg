using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public Text enemyNameText;
    public Text playerNameText;
    public Text levelText; 
    public Slider playerHpSlider;
    public Slider enemyHpSlider;
    //UI text where interaction with user is held
    public Text dialogue;
    // Start is called before the first frame update
    //sets all standard player info
    public void SetPlayerHUD(CurrentPartyData.PartyMember entity)
    {
        playerNameText.text = entity.playerData.name;
        levelText.text = "Lvl. " + entity.currentLevel;
        playerHpSlider.maxValue = entity.currentMaxHealth;
        playerHpSlider.value = entity.currentHealth;
    }
    //sets all standard enemy info
    public void SetEnemyHUD(CombatEnemyData.CommonCombatEnemy entity){
        enemyNameText.text = entity.combatEnemyData.name;
        enemyHpSlider.maxValue= entity.combatEnemyData.health;
        enemyHpSlider.value = entity.combatEnemyData.health;
    }
    //health right now is the only thing that changes in battle so it has it's own method
    public void SetPlayerHp(int hp){
        playerHpSlider.value = hp;
    }
    public void SetEnemyHp(int hp)
    {
        enemyHpSlider.value = hp;
    }
   public void SetText(string message)
    {
        dialogue.text = message;
    }

    
}
