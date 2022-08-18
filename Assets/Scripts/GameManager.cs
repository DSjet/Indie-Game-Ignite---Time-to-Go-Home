using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    
    void Awake(){
        Instance = this;
    }

    void Start(){
        UpdateGameState(GameState.CUTSCENESTATE);
    }

    public void UpdateGameState(GameState newState){
        State = newState;

        switch (newState)
        {
            case GameState.ROAMINGSTATE:
                FindObjectOfType<PlayerMovement>().disableInputState(false);
                break;
            case GameState.BATTLESTATE:
                break;
            case GameState.WINSTATE:
                break;
            case GameState.CUTSCENESTATE:
                FindObjectOfType<PlayerMovement>().disableInputState(true);
                if(FindObjectOfType<Dialogue>() != null) FindObjectOfType<Dialogue>().triggerDialogue();
                break;
            case GameState.DEATHSTATE:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState{
        ROAMINGSTATE,
        BATTLESTATE,
        WINSTATE,
        CUTSCENESTATE,
        DEATHSTATE
    }
}
