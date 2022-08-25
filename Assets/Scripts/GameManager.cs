using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle }
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerController;
    [SerializeField] BattleHandler battleHandler;
    [SerializeField] Camera worldCamera;
    [SerializeField] Camera battleCamera;

    GameState state;

    void Start(){
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

    void Update(){
        if (state == GameState.FreeRoam){
            // Give Control to The Player Controller
            // playerController.HandleUpdate();
        } 
        else if (state == GameState.Battle){ 
            // Give Control to The Battle Controller
            battleHandler.HandleUpdate();
        }
    }
}
