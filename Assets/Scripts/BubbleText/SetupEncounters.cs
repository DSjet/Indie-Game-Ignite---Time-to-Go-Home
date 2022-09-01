using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupEncounters : MonoBehaviour
{
    public BubbleDialogue bubbleDialogue;
    public AddGameObjectFile addSprite;

    void Start(){
        bubbleDialogue.Data = EncounterSceneHandler.DialogueDataSO;
        bubbleDialogue.ContinueWithBattle = EncounterSceneHandler.isContinuingWithBattle;

        foreach(SpriteData datum in EncounterSceneHandler.playerParty){
            addSprite.addSprite(GameObject.Find("PlayerSide").gameObject, datum.image, datum.name);
        }
        foreach(SpriteData datum in EncounterSceneHandler.enemyParty){
            addSprite.addSprite(GameObject.Find("EnemySide").gameObject, datum.image, datum.name);
        }
        bubbleDialogue.triggerDialogue();
    }
}
