using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystems : MonoBehaviour
{
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text nameText;
    public GameObject dialogueWindow;

    private Queue<DialogueData> dialogueArray = new Queue<DialogueData>();
    private Queue<string> sentences = new Queue<string>();

    public bool isOpen = false;

    public void startDialogue(DialogueData[] data){
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
        //End Dialogue
    }
}