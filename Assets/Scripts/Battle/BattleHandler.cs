using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

public class BattleHandler : MonoBehaviour
{
    [SerializeField] List<BattleUnit> playerUnits;
    [SerializeField] List<BattleUnit> enemyUnits;
    [SerializeField] List<BattleHUD> playerHUDs;
    [SerializeField] List<BattleHUD> enemyHUDs;
    [SerializeField] BattleDialogue battleDialogue;

    BattleState state;
    int currentAction;
    int currentMove;

    void Start(){
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        for(var i = 0; i < playerUnits.Count; i++){
            playerUnits[i].Setup();
            playerHUDs[i].SetHUD(playerUnits[i].Char);
        }

        for(var i = 0; i < enemyUnits.Count; i++){
            enemyUnits[i].Setup();
            enemyHUDs[i].SetHUD(enemyUnits[i].Char);
        }

        yield return battleDialogue.TypeDialogue($"{enemyUnits[0].Char.Char.CharName} challengged you to a time duel!");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    private void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(battleDialogue.TypeDialogue("Choose an action"));
        battleDialogue.EnableActionSelector(true);
    }

    private void PlayerMove(){
        state = BattleState.PlayerMove;
        battleDialogue.EnableActionSelector(false);
        battleDialogue.EnableDialogueText(false);
        battleDialogue.EnableMoveSelector(true);
    }

    private void Update() {
        if (state == BattleState.PlayerAction){
            HandleActionSelection();
        } else if (state == BattleState.PlayerMove){
            HandleMoveSelection();
        }
    }


    private void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            if (currentAction < 1){
                ++currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)){
            if(currentAction > 0){
                --currentAction;
            }
        }

        battleDialogue.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z)){
            if (currentAction == 0){
                
            } else if (currentAction == 0){
                // run
            }
        }
    }
    private void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            if (currentMove < playerUnits[0].Char.Skills.Count - 1){
                ++currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)){
            if(currentMove > 0){
                --currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)){
            if (currentMove < playerUnits[0].Char.Skills.Count - 2){
                currentMove += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)){
            if(currentMove > 1){
                currentMove -= 2;
            }
        }

        battleDialogue.UpdateMoveSelection(currentMove, playerUnits[0].Char.Skills[currentMove]); // sementara ini di set ke first character karena belum implement party system

        if (Input.GetKeyDown(KeyCode.Z)){
            battleDialogue.EnableMoveSelector(false);
            battleDialogue.EnableDialogueText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }

    private IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;
        var move = playerUnits[0].Char.Skills[currentMove];
        yield return battleDialogue.TypeDialogue("bla bla bla use this move");
        yield return new WaitForSeconds(1f);

        bool isFainted = enemyUnits[0].Char.TakeDamage(move, playerUnits[0].Char);
        enemyHUDs[0].UpdateHP();

        if (isFainted){
            yield return battleDialogue.TypeDialogue("bla bla bla enemy fainted");
        }
        else {
            StartCoroutine(EnemyMove());
        }
    }

    private IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnits[0].Char.GetRandomSkill();
         yield return battleDialogue.TypeDialogue("bla bla bla use this move");
        yield return new WaitForSeconds(1f);

        bool isFainted = playerUnits[0].Char.TakeDamage(move, playerUnits[0].Char);
        playerHUDs[0].UpdateHP();

        if (isFainted){
            yield return battleDialogue.TypeDialogue("bla bla bla player fainted");
        }
        else {
            PlayerAction();
        }
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
