using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueSystems : MonoBehaviour
{
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text nameText;
    public GameObject dialogueWindow;
    public TimeWorld timer;

    private Queue<DialogueData> dialogueArray = new Queue<DialogueData>();
    private Queue<string> sentences = new Queue<string>();

    public bool isOpen = false;
    private bool isTriggerAfter;
    private UnityEvent ev;

    public void startDialogue(DialogueData[] data, bool isAfterTrigger, UnityEvent ev){
        isTriggerAfter = isAfterTrigger;
        this.ev = ev;
        GameManager.Instance.ChangeState(GameState.CutScene);
        TimeWorld.pauseTimer();
        dialogueWindow.SetActive(true);
        isOpen = true;
        dialogueArray.Clear();
        foreach(DialogueData datum in data){
            dialogueArray.Enqueue(datum);
        }
        iterateDialogue(dialogueArray.Dequeue());
    }

    public void iterateDialogue(DialogueData data){
        nameText.text = data.Name;
        sentences.Clear();

        foreach(string sentence in data.Text){
            sentences.Enqueue(sentence);
        }
        nextSentences();
    }

    public void nextSentences(){
        if(sentences.Count == 0){
            NextDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeAnimation(sentence));
    }

    IEnumerator TypeAnimation(string text){
        dialogueText.text = "";
        foreach(char letter in text.ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
        StartCoroutine(moveSentence());
    }

    IEnumerator moveSentence(){
        yield return new WaitUntil(() => Input.anyKeyDown);
        nextSentences();
    }

    public void NextDialogue(){
        if(dialogueArray.Count == 0){
            EndDialogue();
            return;
        }
        iterateDialogue(dialogueArray.Dequeue());
    }

    public void EndDialogue(){
        isOpen = false;
        dialogueWindow.SetActive(false);
        GameManager.Instance.ChangeState(GameState.FreeRoam);
        TimeWorld.startTimer();
        if(isTriggerAfter){
            ev?.Invoke();
        }
    }
}