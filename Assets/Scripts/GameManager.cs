using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, CutScene }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] PlayerMovement playerController;
    [SerializeField] BattleHandler battleHandler;
    [SerializeField] Camera worldCamera;
    [SerializeField] Camera battleCamera;

    GameState state;

    void Start(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(this);
        }else{
            Destroy(gameObject);
        }
        playerController.OnEncountered += StartBattle;
        battleHandler.OnBattleOver += EndBattle;
    }

    void StartBattle(){
        state = GameState.Battle;
        battleHandler.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        var playerParty = playerController.GetComponent<PartySystem>();
        var enemyParty = playerController.GetComponent<PartySystem>();

        battleHandler.StartBattle(playerParty, enemyParty);
    }

    void EndBattle(bool won){
        state = GameState.FreeRoam;
        battleHandler.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    public void ChangeState(GameState state){
        this.state = state;
    }

    public GameState getState(){
        return state;
    }

    void Update(){
        if (state == GameState.FreeRoam){
            GameObject.FindObjectOfType<PlayerMovement>().disableInputState(false);
            // playerController.HandleUpdate();
        } 
        else if (state == GameState.Battle){ 
            // Give Control to The Battle Controller
            battleHandler.HandleUpdate();
        }else if (state == GameState.CutScene){
            GameObject.FindObjectOfType<PlayerMovement>().disableInputState(true);
        }
    }
}
