using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public Text nameText; 
    public Text levelText; 
    public Slider hpSlider;

    // Start is called before the first frame update
    //sets all standard player info
    public void SetPlayerHUD(CurrentPartyData.PartyMember entity)
    {
        nameText.text = entity.playerData.name;
        levelText.text = "Lvl. " + entity.currentLevel;
        hpSlider.maxValue = entity.currentMaxHealth;
        hpSlider.value = entity.currentHealth;
    }
    //sets all standard enemy info
    public void SetEnemyHUD(CombatEnemyData.CommonCombatEnemy entity){
        nameText.text = entity.combatEnemyData.name;
        hpSlider.maxValue= entity.combatEnemyData.health;
        hpSlider.value = entity.combatEnemyData.health;
    }
    //health right now is the only thing that changes in battle so it has it's own method
    public void SetHp(int hp){
        hpSlider.value = hp;
    }
    
   
    void Start(){
        
        
    }

    
}
