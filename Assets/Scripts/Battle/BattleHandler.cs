using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, ActionSelection, MoveSelection, PerformMove, Busy, BattleOver }

public class BattleHandler : MonoBehaviour
{
    [SerializeField] List<BattleUnit> playerUnits;
    [SerializeField] List<BattleUnit> enemyUnits;
    [SerializeField] BattleDialogue battleDialogue;

    public event Action<bool> OnBattleOver;

    BattleState state;
    int currentAction;
    int currentMove;

    PartySystem playerParty;
    PartySystem enemyParty;

    public void StartBattle(PartySystem playerParty, PartySystem enemyParty){
        this.playerParty = playerParty;
        this.enemyParty = enemyParty;
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        for(var i = 0; i < playerUnits.Count; i++){
            playerUnits[i].Setup(playerParty.GetHealthyCharacter());
        }

        for(var i = 0; i < enemyUnits.Count; i++){
            enemyUnits[i].Setup(enemyParty.GetHealthyCharacter());
        }

        yield return battleDialogue.TypeDialogue($"{enemyUnits[0].Char.Char.CharName} challengged you to a time duel!");
        yield return new WaitForSeconds(1f);

        ActionSelection();
    }

    void BattleOver(bool won){
        state = BattleState.BattleOver;
        OnBattleOver(won);
    }

    private void ActionSelection()
    {
        state = BattleState.ActionSelection;
        StartCoroutine(battleDialogue.TypeDialogue("Choose an action"));
        battleDialogue.EnableActionSelector(true);
    }

    private void MoveSelection(){
        state = BattleState.MoveSelection;
        battleDialogue.EnableActionSelector(false);
        battleDialogue.EnableDialogueText(false);
        battleDialogue.EnableMoveSelector(true);
    }

    public void HandleUpdate() {
        if (state == BattleState.ActionSelection){
            HandleActionSelection();
        } else if (state == BattleState.MoveSelection){
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
            StartCoroutine(PlayerMove());
        }
    }

    private IEnumerator PlayerMove()
    {
        state = BattleState.Busy;
        var move = playerUnits[0].Char.Skills[currentMove];
        yield return RunMove(playerUnits[0], enemyUnits[0], move);

        if(state == BattleState.PerformMove)
            StartCoroutine(EnemyMove());
    }

    IEnumerator RunMove(BattleUnit sourceUnit, BattleUnit targetUnit, Skills skill){
        yield return battleDialogue.TypeDialogue("bla bla bla use this move");

        // Play attack animation
        yield return new WaitForSeconds(1f);

        // Play hit animation
        var damageDetails = targetUnit.Char.TakeDamage(skill, playerUnits[0].Char);
        targetUnit.HUD.UpdateHP();

        if (targetUnit.Char.HP <= 0){
            yield return HandleEnemyFainted(targetUnit);
        }
        else {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator HandleEnemyFainted(BattleUnit faintedUnit){
        yield return battleDialogue.TypeDialogue("bla bla bla enemy fainted");
        // enemy faint animation

        yield return new WaitForSeconds(2f);

        if (!faintedUnit.IsFriendlyUnit){
            // Exp Gain
            int expYield = faintedUnit.Char.Char.ExpYield;
            int enemyLevel = faintedUnit.Char.Level;

            int expGain = Mathf.FloorToInt((expYield * enemyLevel)/7); // based on pokemon formula

            // Check for level up
            while (playerUnits[0].Char.CheckForLevelUp()){
                battleDialogue.TypeDialogue("You character level up");

                // Try to learn a new skill
                var newSkill = playerUnits[0].Char.GetLearnableSkillsMoveAtCurrLevel();
                if (newSkill != null){
                    if (playerUnits[0].Char.Skills.Count < CharacterSO.MaxNumOfMoves){
                        playerUnits[0].Char.LearnSkill(newSkill);
                        yield return battleDialogue.TypeDialogue("learned new move");
                        battleDialogue.SetSkillNames(playerUnits[0].Char.Skills);
                    }
                    else {
                        // Forget an old move
                    }
                }
                // update hud
            }
        }

        CheckForBattleOver(faintedUnit);
    }

    void CheckForBattleOver(BattleUnit faintedUnit){
        if (faintedUnit.IsFriendlyUnit){
            // check if there is still a friendly character in party
            // call BattleOver(false) since the player lost the battle
        } else {
            BattleOver(true);
        }
    }

    private IEnumerator EnemyMove()
    {
        state = BattleState.PerformMove;
        var move = enemyUnits[0].Char.GetRandomSkill();
        yield return RunMove(enemyUnits[0], playerUnits[0], move);
        if(state == BattleState.PerformMove)
            ActionSelection();
    }

    IEnumerator TryToEscape(){
        state = BattleState.Busy;

        //if(condition){
            yield return battleDialogue.TypeDialogue("Successfully escaped");
            BattleOver(true);
        //}

        // else if (condition)
            // yield return battleDialogue.TypeDialogue("You can't escape it's too powerfull");
            // state = BattleState.ActionSelection;
        //}
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
