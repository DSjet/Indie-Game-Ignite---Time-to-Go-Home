using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeEncounterData : MonoBehaviour
{
    public bool isContinuingWithBattle;
    public BubbleDataSO DialogueData;
    public SpriteData[] playerParty;
    public SpriteData[] enemyParty;
    public UnityEvent battleNotCalledEvent;
    public void changeData(){
        EncounterSceneHandler.isContinuingWithBattle = isContinuingWithBattle;
        EncounterSceneHandler.DialogueDataSO = DialogueData;
        EncounterSceneHandler.playerParty = playerParty;
        EncounterSceneHandler.enemyParty = enemyParty;
        EncounterSceneHandler.BattleNotCalledEvent = battleNotCalledEvent;
    }
}
