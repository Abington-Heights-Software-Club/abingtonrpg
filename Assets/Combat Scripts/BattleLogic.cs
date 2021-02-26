using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WIN, LOSS }

public class BattleLogic : MonoBehaviour
{
    
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    
    // Where to instantiate players may just be a different position later
    public Transform playerPad;
    public Transform enemyPad;

    //Holds the current part of the battle from the IEnumuerator
    public BattleState state;

    //Holds UI aspects of players 
    //setHUD sets all values
    //setHP resets the current sliders health values 
    public UI playerUI;
    public UI enemyUI;

    //UI text where interaction with user is held
    public Text dialogue;

    // Holds "info" on players essentially just Enemy objects where you can call accessor and modifer methods to change values
    Enemy playerInfo;
    Enemy enemyInfo;

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
        //GO = GameOject 
        GameObject playerGO = Instantiate(playerPrefab, playerPad);
        //gets all variables from enemy class sets the Enemy object to the current instainated player
        playerInfo = playerGO.GetComponent<Enemy>();

        //same as code above ^^^
        GameObject  enemyGO = Instantiate(enemyPrefab, enemyPad);
        enemyInfo = enemyGO.GetComponent<Enemy>();

        dialogue.text = "You go through a hall and a " + enemyInfo.name + " approaches";

        //sets all of player and enemy info to their tags above them
        playerUI.SetHUD(playerInfo);
        enemyUI.SetHUD(enemyInfo);

        //quirky line of code that gives user some time to digest the current scene before switching to player turn
        yield return new WaitForSeconds(time);


        //switches IEnumerator to PlayerTurn
        state = BattleState.PLAYERTURN ;
        playerTurn();
        
    }

    IEnumerator PlayerAttack(){
        //Attack + check to see if enemy is dead (true/false)
        bool isDead = enemyInfo.TakeDamage(playerInfo.damage);
        //changes displayed health
        enemyUI.SetHp(enemyInfo.currentHP);
        dialogue.text = "The attack is successful";
        yield return new WaitForSeconds(time);
        //if enemy dies during player turn it has to be a win
        //eventually may have to check if all enemy healths = 0
        if(isDead){
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
        dialogue.text = enemyInfo.name + " is attacking";
        yield return new WaitForSeconds(time-1f);
        bool isDead = playerInfo.TakeDamage(enemyInfo.damage);
        playerUI.SetHp(playerInfo.currentHP);
        yield return new WaitForSeconds(time-1.5f);

        if(isDead){
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
            dialogue.text = "You won the battle :)";
        }
        else if(state == BattleState.LOSS){
            dialogue.text = "You lost the battle :(";
        }
    }

    void playerTurn(){
        dialogue.text = "Choose an action ";
    }

    //just increases player health using Heal() method from Enemy class
    //doesn't  have to check for win because you can't win from healing
    IEnumerator PlayerHeal(){
        playerInfo.Heal(healAmount);
        playerUI.SetHp(playerInfo.currentHP);
        dialogue.text = "You healed, wow, congrats";

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
            dialogue.text = "Choose an item";
        }
        pannel.gameObject.SetActive(inventoryMode);
        selection.gameObject.SetActive(!inventoryMode);
    }
}