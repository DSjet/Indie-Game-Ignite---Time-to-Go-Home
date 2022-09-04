using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BattleState { Start, ActionSelection, MoveSelection, PerformMove, Busy, BattleOver }

public class BattleHandler : MonoBehaviour
{
    [SerializeField] BattleDialogue battleDialogue;

    public event Action<bool> OnBattleOver;

    BattleState state;
    int currentAction;
    public int currentMove;

    public PartySystem playerParty;
    public PartySystem enemyParty;
    public Character currentCharacter;
    public AddGameObjectFile addSprite;
    public MoveWorld moveWorld;
    public TimeWorld timeWorld;
    public TriggerSound music;
    public AudioClip audio;

    public bool playerUseDecelerate = false;

    public void EnSceneNext(){
        moveWorld.MoveScene("SceneEncounterData");
    }

    public void StartBattle(){
        foreach(Character c in playerParty.characters){
            c.HPHUD = addSprite.addHP(GameObject.Find("PlayerSide").transform.GetChild(0).gameObject, c.charSO);
        }
        foreach(Character c in enemyParty.characters){
            c.HPHUD = addSprite.addHP(GameObject.Find("EnemySide").transform.GetChild(0).gameObject, c.charSO);
        }
        if(WinnerBattleData.isRetrying){
            TimeWorld.TimeHour = WinnerBattleData.savedTimeHour;
            TimeWorld.TimeMinute = WinnerBattleData.savedTimeMinute;
            TimeWorld.TimeSecond = WinnerBattleData.savedTimeSecond;
            TimeWorld.TimeMiliSecond = WinnerBattleData.savedTimeMiliSecond;
            WinnerBattleData.isRetrying = false;
        }else{
            WinnerBattleData.savedTimeHour = TimeWorld.TimeHour;
            WinnerBattleData.savedTimeMinute = TimeWorld.TimeMinute;
            WinnerBattleData.savedTimeSecond = TimeWorld.TimeSecond;
            WinnerBattleData.savedTimeMiliSecond = TimeWorld.TimeMiliSecond;
        }
        music.changeMusic(audio);
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        yield return battleDialogue.TypeDialogue($"{enemyParty.characters[0].charSO.name} challenged you to a time duel!");
        yield return new WaitForSeconds(1f);
        //-----setup
        playerParty.characters[0].Init();
        foreach(Character c in enemyParty.characters){
            c.Init();
        }
        //------------
        ActionSelection();
    }

    void BattleOver(bool won){
        state = BattleState.BattleOver;
        EnSceneNext();
        OnBattleOver(won);
    }

    private void ActionSelection()
    {
        state = BattleState.ActionSelection;
        StartCoroutine(battleDialogue.TypeDialogue("Choose an action"));
        battleDialogue.EnableActionSelector(true);
        battleDialogue.EnableMoveSelector(false);
    }

    public void MoveSelection(){
        state = BattleState.MoveSelection;
        battleDialogue.EnableActionSelector(false);
        battleDialogue.EnableDialogueText(false);
        battleDialogue.EnableMoveSelector(true);
        currentCharacter = playerParty.characters[0];
        battleDialogue.SetSkillNames(currentCharacter.Skilliard);
        battleDialogue.UpdateMoveSelection();
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
    public void HandleMoveSelection()
    {
        battleDialogue.EnableMoveSelector(false);
        battleDialogue.EnableDialogueText(true);
        StartCoroutine(PlayerMove());
    }

    private IEnumerator PlayerMove()
    {
        state = BattleState.Busy;
        var move = currentCharacter.Skilliard[currentMove];
        yield return RunMove(currentCharacter, enemyParty.characters[0], move);
    }

    IEnumerator RunMove(Character sourceUnit, Character targetUnit, Skills skill){
        if(skill.ManaCost > 0){
            timeWorld.changeTimer(-10);
        }
        var damageDetails = targetUnit.TakeDamage(skill, targetUnit, playerUseDecelerate);
        targetUnit.UpdateHPUD();
        // Play hit animation
        if(playerUseDecelerate && sourceUnit.charSO.CharName != "Player"){
            yield return battleDialogue.TypeDialogue($"{targetUnit.charSO.CharName} blocked half {skill.Skill.SkillName} from {sourceUnit.charSO.CharName} using Decelerate.");
            playerUseDecelerate = false;
        }else{
            yield return battleDialogue.TypeDialogue($"{sourceUnit.charSO.CharName} attacks with {skill.Skill.SkillName}.");
        }
        // Play attack animation
        yield return new WaitForSeconds(1f);
        if(skill.Skill.SkillName == "Decelerate"){
            playerUseDecelerate = true;
        }

        if (targetUnit.HP <= 0){
            WinnerBattleData.Winner = sourceUnit;
            yield return HandleEnemyFainted(targetUnit);
        } else if(sourceUnit.charSO.CharName == "You"){
            StartCoroutine(EnemyMove());
        } else if(sourceUnit.charSO.CharName != "You"){
            ActionSelection();
        }
    }

    private IEnumerator EnemyMove()
    {
        state = BattleState.PerformMove;
        var move = enemyParty.characters[0].GetRandomSkill();
        yield return RunMove(enemyParty.characters[0], playerParty.characters[0], move);
    }

    IEnumerator HandleEnemyFainted(Character faintedUnit){
        yield return battleDialogue.TypeDialogue($"{faintedUnit.charSO.CharName} has fainted!");
        // enemy faint animation

        yield return new WaitForSeconds(2f);

        /*if (!faintedUnit.IsFriendlyUnit){
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
                    if (playerUnits[0].Char.Skilliard.Count < CharacterSO.MaxNumOfMoves){
                        playerUnits[0].Char.LearnSkill(newSkill);
                        yield return battleDialogue.TypeDialogue("learned new move");
                        battleDialogue.SetSkillNames(playerUnits[0].Char.Skilliard);
                    }
                    else {
                        // Forget an old move
                    }
                }
                // update hud
            }
        }*/

        CheckForBattleOver(faintedUnit);
    }

    void CheckForBattleOver(Character faintedUnit){
        if (faintedUnit.charSO.CharName == "You"){
            // check if there is still a friendly character in party
            BattleOver(false);
        } else {
            BattleOver(true);
        }
    }

    public void failEscape(){
        StartCoroutine(TryToEscape());
    }

    IEnumerator TryToEscape(){
        state = BattleState.Busy;

        //if(condition){
            yield return battleDialogue.TypeDialogue("Failed to escape...");
            state = BattleState.MoveSelection;
            MoveSelection();

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
