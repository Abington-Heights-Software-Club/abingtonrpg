using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatEnemyData
{
    public static int enemyPartySize = 4;
    public static bool isBossFight = false;
    public static CommonCombatEnemy[] commonCombatEnemyParty = new CommonCombatEnemy[enemyPartySize];
    public static BossData boss { get; set; }
    public static int bossCurrentHealth { get; set; }

    //Static method that sets up the boss for the fight. 
    public static void setCombatEnemy(bool isBoss, string[] ids)
    {
        isBossFight = isBoss;
        if(isBoss)
        {
            boss = new BossData(ids[0]);
            bossCurrentHealth = boss.health;
        }
        else
        {
            int count = 0;
            foreach(string id in ids)
            {
                commonCombatEnemyParty[count] = new CommonCombatEnemy(id);
                count++;
            }
        }
    }

    public class CommonCombatEnemy {
        public CommonEnemyData combatEnemyData;
        public int currentHealth;
        public CommonCombatEnemy(string id)
        {
            combatEnemyData = new CommonEnemyData(id);
            currentHealth = combatEnemyData.health;
        }
    }
}