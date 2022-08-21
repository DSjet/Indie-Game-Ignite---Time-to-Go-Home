using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle}
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerController;

    GameState state;

    void Update(){
        if (state == GameState.FreeRoam){

        } 
        else if (state == GameState.Battle){

        }
    }
}
