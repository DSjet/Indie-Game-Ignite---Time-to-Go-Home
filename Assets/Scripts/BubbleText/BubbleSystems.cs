using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class BubbleSystems : MonoBehaviour
{
    public GameObject bubbleBox;
    public GameObject HUDHandler;
    public PartySystem playerPartySystem;
    public PartySystem enemyPartySystem;
    public TMP_Text Text;
    public MoveWorld moveScript;
    private RectTransform textObject;
    public GameObject battleHUD;
    public bool isDialogueOpen = false;
    private bool startBattle = false;


    private RectTransform bgRenderer;
    private Queue<BubbleDataType> Dialogues = new Queue<BubbleDataType>();
    private Queue<string> dialogueText = new Queue<string>();
    private float offset = 0f;
    private Vector2 initialTextSize;
    private string moveSceneTo;

    void Awake(){
        bgRenderer = bubbleBox.GetComponent<RectTransform>();
        textObject = Text.gameObject.GetComponent<RectTransform>();
        initialTextSize = textObject.rect.size;
        moveSceneTo = EncounterSceneHandler.moveSceneTo;
    }

    public void startDialogue(BubbleDataSO data, bool startBattle){
        GameManager.Instance.ChangeState(GameState.CutScene);
        Dialogues.Clear();
        TimeWorld.pauseTimer();
        foreach(BubbleDataType dialog in data.Dialogue){
            Dialogues.Enqueue(dialog);
        }
        bubbleBox.SetActive(true);
        offset = data.yOffset;
        isDialogueOpen = true;
        this.startBattle = startBattle;
        iterateDialogue(Dialogues.Dequeue());
    }

    public void iterateDialogue(BubbleDataType data){
        if(data.PlayAudio == null){
            GameObject parent =  GameObject.Find(data.Tag);
            GameObject points = parent.transform.GetChild(0).gameObject;
            Vector3 newPosition = new Vector3(points.transform.position.x, points.transform.position.y, 0);
            HUDHandler.transform.position = newPosition;
            foreach(string text in data.text){
                dialogueText.Enqueue(text);
            }
        }else{
            SoundManager.Instance.PlaySound(data.PlayAudio);
        }
        showNextDialogue();
    }

    IEnumerator moveToNextSentence(){
        yield return new WaitUntil(()=> Input.anyKey);
        showNextDialogue();
    }

    public void showNextDialogue(){
        if(dialogueText.Count == 0){
            nextDialogue();
            return;
        }
        StopAllCoroutines();
        string sentence = dialogueText.Dequeue();
        textObject.sizeDelta = initialTextSize;
        Text.SetText(sentence);
        Text.ForceMeshUpdate();
        Vector2 textSize = Text.GetRenderedValues(false);
        Vector2 padding = new Vector2(16f,16f);
        bgRenderer.sizeDelta = textSize + padding;
        Text.SetText("");
        textObject.sizeDelta = textSize;
        StartCoroutine(typeText(sentence));
    }

    IEnumerator typeText(string text){
        foreach(char letter in text.ToCharArray()){
            Text.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
        StartCoroutine(moveToNextSentence());
    }

    public void nextDialogue(){
        if(Dialogues.Count == 0){
            EndDialogue();
            return;
        }
        iterateDialogue(Dialogues.Dequeue());
    }

    public void EndDialogue(){
        bubbleBox.SetActive(false);
        isDialogueOpen = false;
        if(startBattle){
            battleHUD.SetActive(true);
            GameManager.Instance.ChangeState(GameState.Battle);
            GameObject.FindObjectOfType<BattleHandler>().StartBattle();
        }else{
            if(moveSceneTo != null || moveSceneTo != ""){
                moveScript.MoveScene(moveSceneTo);
                GameManager.Instance.ChangeState(GameState.FreeRoam);
            }
        }
        TimeWorld.startTimer();
    }
}
