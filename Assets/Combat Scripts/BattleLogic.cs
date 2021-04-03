using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {START, PLAYERSELECT, PLAYERCHOOSEENEMY, PLAYERINACTION, ENEMYTURN, WIN, LOSS }

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
    //int indicating what the user's arrow is currently selecting for an attack
    private int currentSelectedEnemy = 0;

    //bool array keeping track of which players are dead
    bool[] playersAlive = { true, true, true, true };
    //bool array keeping track of which enemies are dead
    bool[] enemiesAlive = { true, true, true, true };
    void Start()
    {
        //set empty spots as dead
        int i = 0;
        foreach (CombatEnemyData.CommonCombatEnemy entity in CombatEnemyData.commonCombatEnemyParty)
        {
            if (entity == null)
            {
                enemiesAlive[i] = false;
            }
            i++;
        }
        i = 0;
        foreach (CurrentPartyData.PartyMember entity in CurrentPartyData.party)
        {
            if (entity == null)
            {
                playersAlive[i] = false;
            }
            i++;
        }
        //sets the IEnumerator state to Start to begin a battle
        state = BattleState.START;
        //syntax to call an IEnumerator
        StartCoroutine(SetUpBattle());
        //
        pannel.gameObject.SetActive(inventoryMode);
    }
    private void Update()
    {
        if(state == BattleState.PLAYERCHOOSEENEMY)
        {
            UI.SetText("Select An Enemy");
            if (Input.GetKeyDown("a"))
            {
                currentSelectedEnemy--;
                while(currentSelectedEnemy == -1 || enemiesAlive[currentSelectedEnemy] == false)
                {
                    if (currentSelectedEnemy == -1)
                    {
                        currentSelectedEnemy = CombatEnemyData.currentEnemyPartySize - 1;
                    }
                    else
                    {
                        currentSelectedEnemy--;
                    }
                }
                //Debug.Log("Current select: " + currentSelectedEnemy);
                UI.changeArrow(currentSelectedEnemy, true);
            }
            if(Input.GetKeyDown("d"))
            {
                currentSelectedEnemy++;
                while (currentSelectedEnemy == CombatEnemyData.currentEnemyPartySize || enemiesAlive[currentSelectedEnemy] == false)
                {
                    if (currentSelectedEnemy == CombatEnemyData.currentEnemyPartySize)
                    {
                        currentSelectedEnemy = 0;
                    }
                    else
                    {
                        currentSelectedEnemy++;
                    }
                }
                //Debug.Log("Current select: " + currentSelectedEnemy);
                UI.changeArrow(currentSelectedEnemy, true);
            }
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                state = BattleState.PLAYERINACTION;
                UI.changeArrow(currentSelectedEnemy, false);
                StartCoroutine(PlayerAttack(currentSelectedEnemy));
            }
        }
    }

    //Is called from start method
    IEnumerator SetUpBattle() {
        //sets all of player and enemy info to their tags above them
        UI.SetPlayerHUD();
        UI.SetEnemyHUD();
        UI.SetText("An enemy approaches you!");
        //quirky line of code that gives user some time to digest the current scene before switching to player turn
        yield return new WaitForSeconds(time);


        //switches IEnumerator to PlayerTurn
        state = BattleState.PLAYERSELECT;
        playerTurn();
        
    }

    IEnumerator PlayerAttack(int target){
        //Debug.Log("Attack Started");
        System.Random r = new System.Random();
        //damage is random int between lower and upper damage
        //Next upper bound is exclusive which is why the + 1 is used
        int damage = r.Next(CurrentPartyData.party[currentPlayer].currentLowDamage, CurrentPartyData.party[currentPlayer].currentHighDamage + 1);
        Debug.Log(damage);
        //sets health to 0 if damage makes health negative
        if(CombatEnemyData.commonCombatEnemyParty[target].currentHealth - damage <= 0)
        {
            CombatEnemyData.commonCombatEnemyParty[target].currentHealth = 0;
        }
        else
        {
            CombatEnemyData.commonCombatEnemyParty[target].currentHealth -= damage;
        }
        UI.RefreshEnemyHp();
        UI.SetText("The attack is successful");
        yield return new WaitForSeconds(time);
        bool gameOver = true;
        for(int i = 0; i < CurrentPartyData.currentPartySize; i++)
        {
            if(enemiesAlive[i] == true)
            {
                if (CombatEnemyData.commonCombatEnemyParty[i].currentHealth == 0)
                {
                    enemiesAlive[i] = false;
                    UI.enemyDead(i);
                    UI.SetText(CombatEnemyData.commonCombatEnemyParty[i].combatEnemyData.name + " has passed out!");
                    yield return new WaitForSeconds(time);
                }
                else
                {
                    gameOver = false;
                }
            }
        }
        if(gameOver)
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
                state = BattleState.PLAYERSELECT;
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
        UI.RefreshPlayerHp();
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
                state = BattleState.PLAYERSELECT;
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
        UI.RefreshPlayerHp();
        UI.SetText(CurrentPartyData.party[currentPlayer].playerData.name + " healed, wow, congrats");

        yield return new WaitForSeconds(time);
        currentPlayer++;
        if (currentPlayer < CurrentPartyData.currentPartySize)
        {
            state = BattleState.PLAYERSELECT;
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
        if(state == BattleState.PLAYERSELECT){
            currentSelectedEnemy = 0;
            while(!enemiesAlive[currentSelectedEnemy])
            {
                currentSelectedEnemy++;
            }
            state = BattleState.PLAYERCHOOSEENEMY;
            UI.changeArrow(currentSelectedEnemy, true);
        }

    }
    //Same concept as OnAttackButton ^^^
    public void OnHealButton(){

        if(state ==BattleState.PLAYERSELECT){
            state = BattleState.PLAYERINACTION;
            StartCoroutine(PlayerHeal());
        }
        
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