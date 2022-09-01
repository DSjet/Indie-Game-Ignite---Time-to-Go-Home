using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    private GameManager gameManager;

    void Start(){
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void changeStateToFreeRoam(){
        gameManager.ChangeState(GameState.FreeRoam);
    }

    public void changeStateToBattle(){
        gameManager.ChangeState(GameState.Battle);
    }

    public void changeStateToCutScene(){
        gameManager.ChangeState(GameState.CutScene);
    }
}
