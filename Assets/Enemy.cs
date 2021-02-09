using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //takes in basic instance variables
    public string name;
    public int level; 

    public int damage; 

    public int maxHp;
    public int currentHP;
    //changes health value and returns true if enemy died
    public bool TakeDamage(int dmg){
        currentHP -=dmg;

        if(currentHP<=0){
            return true;
        }
        return false;
    }
    //adds to health instance var
    public void Heal(int amount){
        currentHP +=amount;
        if(currentHP>maxHp)
            currentHP= maxHp;
    }
}
