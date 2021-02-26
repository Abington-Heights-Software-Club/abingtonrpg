using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatEnemyData
{
    public static int enemyPartySize = 4;
    public static bool isBossFight = false;
    public static CommonCombatEnemy[] commonCombatEnemyParty = new CommonCombatEnemy[enemyPartySize];
    public static BossData boss { get; private set; }
    public static int bossCurrentHealth { get; private set; }

    //Static method that sets up the boss for the fight. 
    public static void setCombatEnemy(bool isBoss, string id)
    {
        isBossFight = isBoss;
        if(isBoss)
        {
            // boss = new BossData(id);
            // bossCurrentHealth = boss.health;
        }
        else
        {

        }
    }

    public class CommonCombatEnemy {
        public CommonEnemyData combatEnemyData;
        public int currentHealth;
        public CommonCombatEnemy(string id)
        {
            // combatEnemyData = new CommonEnemyData(id);
            // currentHealth = combatEnemyData.health;
        }
    }
}