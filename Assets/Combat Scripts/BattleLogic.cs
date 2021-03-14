using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WIN, LOSS }

public class BattleLogic : MonoBehaviour
{

    //Holds the current part of the battle from the IEnumuerator
    public BattleState state;

    //Holds UI aspects of players 
    //setHUD sets all values
    //setHP resets the current sliders health values 
    public UI UI;



    //time it takes to switch turn 
    public float time = 2f; 
    //amount of health heal button gives
    public int healAmount = 35;

    private bool inventoryMode;
    public GameObject pannel; 
    public GameObject selection;
    //int indicating which player is up to move
    private int currentPlayer = 0;
    //int indicating which enemy is up to move
    private int currentEnemy = 0;
    void Start()
    {
        //sets the IEnumerator state to Start to begin a battle
        state = BattleState.START;
        //syntax to call an IEnumerator
        StartCoroutine(SetUpBattle());
        //
        pannel.gameObject.SetActive(inventoryMode);
    }

    //Is called from start method
    IEnumerator SetUpBattle() {
        //sets all of player and enemy info to their tags above them
        UI.SetPlayerHUD(CurrentPartyData.party[0]);
        UI.SetEnemyHUD(CombatEnemyData.commonCombatEnemyParty[0]);
        UI.SetText("You go through a hall and a " + CombatEnemyData.commonCombatEnemyParty[0].combatEnemyData.name + " approaches");
        //quirky line of code that gives user some time to digest the current scene before switching to player turn
        yield return new WaitForSeconds(time);


        //switches IEnumerator to PlayerTurn
        state = BattleState.PLAYERTURN ;
        playerTurn();
        
    }

    IEnumerator PlayerAttack(){
        System.Random r = new System.Random();
        //damage is random int between lower and upper damage
        //Next upper bound is exclusive which is why the + 1 is used
        int damage = r.Next(CurrentPartyData.party[currentPlayer].currentLowDamage, CurrentPartyData.party[currentPlayer].currentHighDamage + 1);
        Debug.Log(damage);
        //sets health to 0 if damage makes health negative
        if(CombatEnemyData.commonCombatEnemyParty[0].currentHealth - damage <= 0)
        {
            CombatEnemyData.commonCombatEnemyParty[0].currentHealth = 0;
        }
        else
        {
            CombatEnemyData.commonCombatEnemyParty[0].currentHealth -= damage;
        }
        UI.SetEnemyHp(CombatEnemyData.commonCombatEnemyParty[0].currentHealth);
        UI.SetText("The attack is successful");
        yield return new WaitForSeconds(time);
        //if enemy dies during player turn it has to be a win
        //eventually may have to check if all enemy healths = 0
        if(CombatEnemyData.commonCombatEnemyParty[0].currentHealth == 0)
        {
            state = BattleState.WIN;
            EndBattle();
        }
        //continue to next player or enemy turn
        else{
            currentPlayer++;
            //Debug.Log("currentPlayer: " + currentPlayer + " currentPartySize: " + CurrentPartyData.currentPartySize);
            if(currentPlayer < CurrentPartyData.currentPartySize)
            {
                state = BattleState.PLAYERTURN;
                playerTurn();
            }
            else
            {
                currentPlayer = 0;
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }

    }
//Same logic as player turn ^^^
    IEnumerator EnemyTurn(){
        System.Random r = new System.Random();
        int enemeyTarget = r.Next(CurrentPartyData.currentPartySize);
        Debug.Log("Enemy selected: " + enemeyTarget);
        UI.SetText(CombatEnemyData.commonCombatEnemyParty[currentEnemy].combatEnemyData.name + " is attacking " + CurrentPartyData.party[enemeyTarget].playerData.name);
        yield return new WaitForSeconds(time-1f);
        //Next upper bound is exclusive which is why the + 1 is used
        int damage = r.Next(CombatEnemyData.commonCombatEnemyParty[currentEnemy].combatEnemyData.low_damage, CombatEnemyData.commonCombatEnemyParty[currentEnemy].combatEnemyData.high_damage + 1);
        Debug.Log("Enemy damage: " + damage);
        if(CurrentPartyData.party[enemeyTarget].currentHealth - damage < 0)
        {
            CurrentPartyData.party[enemeyTarget].currentHealth = 0;
        }
        else
        {
            CurrentPartyData.party[enemeyTarget].currentHealth -= damage;
        }
        UI.SetPlayerHp(CurrentPartyData.party[0].currentHealth);
        yield return new WaitForSeconds(time-1.5f);

        if(CurrentPartyData.party[0].currentHealth == 0){
            state = BattleState.LOSS;
            EndBattle();
        }
        else{
            currentEnemy++;
            if (currentEnemy < CombatEnemyData.currentEnemyPartySize)
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
            else
            {
                currentEnemy = 0;
                state = BattleState.PLAYERTURN;
                playerTurn();
            }

        }
    }

    //Checks to see who won the battle, displays info, and resets the CombatEnemyData properties
    void EndBattle(){
        if(state == BattleState.WIN){
            UI.SetText("You won the battle :)");
        }
        else if(state == BattleState.LOSS){
            UI.SetText("You lost the battle :(");
        }
        CombatEnemyData.resetCombatEnemy();
    }

    void playerTurn(){
        UI.SetText("Choose an action for " + CurrentPartyData.party[currentPlayer].playerData.name);
    }

    //just increases player health using Heal() method from Enemy class
    //doesn't  have to check for win because you can't win from healing
    IEnumerator PlayerHeal(){
        if(CurrentPartyData.party[currentPlayer].currentHealth + healAmount > CurrentPartyData.party[currentPlayer].currentMaxHealth)
        {
            CurrentPartyData.party[currentPlayer].currentHealth = CurrentPartyData.party[currentPlayer].currentMaxHealth;
        }
        else
        {
            CurrentPartyData.party[currentPlayer].currentHealth += healAmount;
        }
        UI.SetPlayerHp(CurrentPartyData.party[0].currentHealth);
        UI.SetText(CurrentPartyData.party[currentPlayer].playerData.name + " healed, wow, congrats");

        yield return new WaitForSeconds(time);
        currentPlayer++;
        if (currentPlayer < CurrentPartyData.currentPartySize)
        {
            state = BattleState.PLAYERTURN;
            playerTurn();
        }
        else
        {
            currentPlayer = 0;
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    //Used in OnClick function in UI; This can be found with the inspector
     public void OnAttackButton(){
         //If the player spams button when its not their turn it will do nothing
        if(state != BattleState.PLAYERTURN){
            return;
        }
        //Go through player attack aka check if enemy alive and move to next IEnumerator
        StartCoroutine(PlayerAttack());
    }
    //Same concept as OnAttackButton ^^^
    public void OnHealButton(){

        if(state != BattleState.PLAYERTURN){
            return;
        }
        StartCoroutine(PlayerHeal());
    }
    public void HideSelectionShowInventory(){
        inventoryMode = !inventoryMode;
        if(inventoryMode == true){
            UI.SetText("Choose an item");
        }
        pannel.gameObject.SetActive(inventoryMode);
        selection.gameObject.SetActive(!inventoryMode);
    }
}