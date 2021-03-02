using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public Text nameText; 
    public Text levelText; 
    public Slider hpSlider;
    //sets all standard enemy info
    public void SetHUD(Enemy entity){
        nameText.text = entity.name;
        levelText.text = "Lvl " + entity.level+".";
        hpSlider.maxValue= entity.maxHp;
        hpSlider.maxValue = entity.currentHP;
    }
    //health right now is the only thing that changes in battle so it has it's own method
    public void SetHp(int hp){
        hpSlider.value = hp;
    }

 
    
}
