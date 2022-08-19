using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleHandler : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHUD playerHUD;

    void Start(){
        SetupBattle();
    }

    private void SetupBattle()
    {
        playerUnit.Setup();
        playerHUD.SetHUD(playerUnit.Char);
    }

    // public GameObject playerPrefab;
    // public GameObject enemyPrefab;

    // StatsManager playerStats;
    // StatsManager enemyStats;

    // public Transform playerBattleStation;
    // public Transform enemyBattleStation;

    // public BattleState state;
    // BattleHUD playerHUD;
    // BattleHUD enemyHUD;

    // void Awake(){
    //     playerHUD = playerPrefab.GetComponentInChildren<BattleHUD>();
    //     enemyHUD = enemyPrefab.GetComponentInChildren<BattleHUD>();
    // }

    // void Start()
    // {
    //     state = BattleState.START;
    //     StartCoroutine(SetupBattle());
    // }

    // IEnumerator SetupBattle()
    // {

    //     GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
    //     playerStats = playerGO.GetComponent<StatsManager>();
    //     GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
    //     enemyStats = enemyGO.GetComponent<StatsManager>();

    //     playerHUD.SetHUD(playerStats);
    //     enemyHUD.SetHUD(enemyStats);

    //     yield return new WaitForSeconds(2f);

    //     state = BattleState.PLAYERTURN;
    //     PlayerTurn();
    // }

    // IEnumerator PlayerAttack()
    // {
    //     bool isDead = enemyStats.TakeDamage(playerStats.Damage);

    //     enemyHUD.SetHP(enemyStats.CurrentHP);
    //     yield return new WaitForSeconds(2f);

    //     if (isDead)
    //     {
    //         state = BattleState.WON;
    //         EndBattle();
    //     } else 
    //     {
    //         state = BattleState.ENEMYTURN;
    //         StartCoroutine(EnemyTurn());
    //     }
    // }

    // IEnumerator EnemyTurn()
    // {
    //     // add some kind of animation or dialogues

    //     yield return new WaitForSeconds(1f);

    //     bool isDead = playerStats.TakeDamage(enemyStats.Damage);

    //     playerHUD.SetHP(playerStats.CurrentHP);

    //     yield return new WaitForSeconds(1f);

    //     if (isDead){
    //         state = BattleState.LOST;
    //         EndBattle();
    //     } else {
    //         state = BattleState.PLAYERTURN;
    //         PlayerTurn();
    //     }
    // }

    // void EndBattle()
    // {
    //     if(state == BattleState.WON)
    //     {
    //         // display winning text
    //     } else if (state == BattleState.LOST) {
    //         // display losing text
    //     }
    // }

    // private void PlayerTurn()
    // {

    // }

    // public void OnAttackButton()
    // {
    //     if (state != BattleState.PLAYERTURN)
    //         return;

    //     StartCoroutine(PlayerAttack());
    // }

    // public void OnHealButton()
    // {
    //     if (state != BattleState.PLAYERTURN)
    //         return;

    //     StartCoroutine(PlayerHeal());
    // }

    // IEnumerator PlayerHeal()
    // {
    //     playerStats.Heal(5);

    //     playerHUD.SetHP(playerStats.CurrentHP);

    //     yield return new WaitForSeconds(2f);

    //     state = BattleState.ENEMYTURN;
    //     StartCoroutine(EnemyTurn());
    // }
}
