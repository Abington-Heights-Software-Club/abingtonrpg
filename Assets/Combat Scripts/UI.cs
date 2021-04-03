using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    //All of the players UI elements
    public Slider[] playerHpSliders = new Slider[4];
    public Text[] playerNameTexts = new Text[4];
    public Text[] playerLevelTexts = new Text[4];
    public GameObject[] playerPads = new GameObject[4];
    public GameObject[] playerSprites = new GameObject[4];

    //All of the enemies UI elements
    public Slider[] enemyHpSliders = new Slider[4];
    public Text[] enemyNameTexts = new Text[4];
    public GameObject[] enemyPads = new GameObject[4];
    public GameObject[] enemySprites = new GameObject[4];
   
    //UI text where interaction with user is held
    public Text dialogue;

    public GameObject arrow;
    public int positionAboveEnemy;

    private void Start()
    {
        changeArrow(0, false);
    }
    //sets all standard player info
    public void SetPlayerHUD()
    {
        int i = 0;
        foreach(CurrentPartyData.PartyMember entity in CurrentPartyData.party)
        {
            if(entity == null)
            {
                playerHpSliders[i].gameObject.SetActive(false);
                playerNameTexts[i].gameObject.SetActive(false);
                playerLevelTexts[i].gameObject.SetActive(false);
                playerPads[i].SetActive(false);
                playerSprites[i].SetActive(false);
            }
            else
            {
                playerNameTexts[i].text = entity.playerData.name;
                playerLevelTexts[i].text = "Lvl. " + entity.currentLevel;
                playerHpSliders[i].maxValue = entity.currentMaxHealth;
                playerHpSliders[i].value = entity.currentHealth;
            }
            i++;
        }
        
    }
    //sets all standard enemy info
    public void SetEnemyHUD(){
        int i = 0;
        foreach (CombatEnemyData.CommonCombatEnemy entity in CombatEnemyData.commonCombatEnemyParty)
        {
            if (entity == null)
            {
                enemyHpSliders[i].gameObject.SetActive(false);
                enemyNameTexts[i].gameObject.SetActive(false);
                enemyPads[i].SetActive(false);
                enemySprites[i].SetActive(false);
            }
            else
            {
                enemyNameTexts[i].text = entity.combatEnemyData.name;
                enemyHpSliders[i].maxValue = entity.combatEnemyData.health;
                enemyHpSliders[i].value = entity.combatEnemyData.health;
            }
            i++;
        }
    }
    public void RefreshPlayerHp(){
        int i = 0;
        foreach (CurrentPartyData.PartyMember entity in CurrentPartyData.party)
        {
            if (entity != null)
            {
                playerHpSliders[i].value = entity.currentHealth;
            }
            i++;
        }
    }
    public void RefreshEnemyHp()
    {
        int i = 0;
        foreach (CombatEnemyData.CommonCombatEnemy entity in CombatEnemyData.commonCombatEnemyParty)
        {
            if (entity != null)
            {
                enemyHpSliders[i].value = entity.currentHealth;
            }
            i++;
        }
    }
    public void playerDead(int position)
    {
        playerHpSliders[position].gameObject.SetActive(false);
        playerNameTexts[position].gameObject.SetActive(false);
        playerLevelTexts[position].gameObject.SetActive(false);
        playerPads[position].SetActive(false);
        playerSprites[position].SetActive(false);
    }
    public void enemyDead(int position)
    {
        enemyHpSliders[position].gameObject.SetActive(false);
        enemyNameTexts[position].gameObject.SetActive(false);
        enemyPads[position].SetActive(false);
        enemySprites[position].SetActive(false);
    }
    public void SetText(string message)
    {
        dialogue.text = message;
    }
    //Changes the position of the arrow to another enemy
    public void changeArrow(int enemySelected, bool isVisible)
    {
        arrow.SetActive(isVisible);
        arrow.transform.position = new Vector2(enemySprites[enemySelected].transform.position.x, enemySprites[enemySelected].transform.position.y + positionAboveEnemy);
    }
}
