using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChanger : MonoBehaviour
{
    public void changeRoamState(){
        GameManager.Instance.UpdateGameState(GameManager.GameState.ROAMINGSTATE);
    }
    public void changeCutsceneState(){
        GameManager.Instance.UpdateGameState(GameManager.GameState.CUTSCENESTATE);
    }
    public void changeBattleState(){
        GameManager.Instance.UpdateGameState(GameManager.GameState.BATTLESTATE);
    }
    public void changeWinState(){
        GameManager.Instance.UpdateGameState(GameManager.GameState.WINSTATE);
    }
    public void changeDeathState(){
        GameManager.Instance.UpdateGameState(GameManager.GameState.DEATHSTATE);
    }
}
