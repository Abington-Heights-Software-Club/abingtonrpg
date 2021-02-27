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

    // Start is called before the first frame update
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
        int damage = r.Next(CurrentPartyData.party[0].currentLowDamage, CurrentPartyData.party[0].currentHighDamage);
        Debug.Log(damage);
        if(CombatEnemyData.commonCombatEnemyParty[0].currentHealth - damage < 0)
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
        //continue to enemy turn 
        else{
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }
//Same logic as player turn ^^^
    IEnumerator EnemyTurn(){
        System.Random r = new System.Random();
        UI.SetText(CombatEnemyData.commonCombatEnemyParty[0].combatEnemyData.name + " is attacking");
        yield return new WaitForSeconds(time-1f);
        int damage = r.Next(CombatEnemyData.commonCombatEnemyParty[0].combatEnemyData.low_damage, CombatEnemyData.commonCombatEnemyParty[0].combatEnemyData.high_damage);
        Debug.Log(damage);
        if(CurrentPartyData.party[0].currentHealth - damage < 0)
        {
            CurrentPartyData.party[0].currentHealth = 0;
        }
        else
        {
            CurrentPartyData.party[0].currentHealth -= damage;
        }
        UI.SetPlayerHp(CurrentPartyData.party[0].currentHealth);
        yield return new WaitForSeconds(time-1.5f);

        if(CurrentPartyData.party[0].currentHealth == 0){
            state = BattleState.LOSS;
            EndBattle();
        }
        else{
            state = BattleState.PLAYERTURN;
            playerTurn();
        }
    }

    //Checks to see how won the battle and displays that info
    void EndBattle(){
        if(state == BattleState.WIN){
            UI.SetText("You won the battle :)");
        }
        else if(state == BattleState.LOSS){
            UI.SetText("You lost the battle :(");
        }
    }

    void playerTurn(){
        UI.SetText("Choose an action");
    }

    //just increases player health using Heal() method from Enemy class
    //doesn't  have to check for win because you can't win from healing
    IEnumerator PlayerHeal(){
        if(CurrentPartyData.party[0].currentHealth + healAmount > CurrentPartyData.party[0].currentMaxHealth)
        {
            CurrentPartyData.party[0].currentHealth = CurrentPartyData.party[0].currentMaxHealth;
        }
        else
        {
            CurrentPartyData.party[0].currentHealth += healAmount;
        }
        UI.SetPlayerHp(CurrentPartyData.party[0].currentHealth);
        UI.SetText("You healed, wow, congrats");

        yield return new WaitForSeconds(time);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
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