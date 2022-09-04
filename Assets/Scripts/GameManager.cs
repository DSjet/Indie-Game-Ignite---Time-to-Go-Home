using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, CutScene }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    GameState state;

    void Start(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(this);
        }else{
            Destroy(gameObject);
        }
    }

    public void ChangeState(GameState state){
        this.state = state;
    }

    public GameState getState(){
        return state;
    }
}
