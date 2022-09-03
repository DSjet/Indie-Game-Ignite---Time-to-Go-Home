using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupEncounters : MonoBehaviour
{
    public BubbleDialogue bubbleDialogue;
    public AddGameObjectFile addSprite;
    public BattleHandler battleHandler;
    public PartySystem playerParty;
    public PartySystem enemyParty;

    void Start(){
        bubbleDialogue.Data = EncounterSceneHandler.DialogueDataSO;
        bubbleDialogue.ContinueWithBattle = EncounterSceneHandler.isContinuingWithBattle;

        foreach(SpriteData datum in EncounterSceneHandler.playerParty){
            addSprite.addSprite(GameObject.Find("PlayerSide").gameObject, datum.image, datum.name);
            Character ch = new Character();
            ch.charSO = datum.characterSO;
            ch.level = datum.level;
            playerParty.characters.Add(ch);
        }
        battleHandler.playerParty = playerParty;
        foreach(SpriteData datum in EncounterSceneHandler.enemyParty){
            addSprite.addSprite(GameObject.Find("EnemySide").gameObject, datum.image, datum.name);
            Character ch = new Character();
            ch.charSO = datum.characterSO;
            ch.level = datum.level;
            enemyParty.characters.Add(ch);
        }
        battleHandler.enemyParty = enemyParty;
        bubbleDialogue.triggerDialogue();
    }
}
