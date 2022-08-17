using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleSystems : MonoBehaviour
{
    private GameObject bubbleBox;
    private TextMeshPro Text;
    public bool isDialogueOpen = false;

    private SpriteRenderer bgRenderer;
    private Queue<BubbleDataType> Dialogues = new Queue<BubbleDataType>();
    private Queue<string> dialogueText = new Queue<string>();
    private float offset = 0f;

    void Awake(){
        bubbleBox = transform.Find("Box").gameObject;
        Text = bubbleBox.transform.Find("Text").GetComponent<TextMeshPro>();
        bgRenderer = bubbleBox.GetComponent<SpriteRenderer>();
    }

    public void startDialogue(BubbleDataSO data){
        Dialogues.Clear();
        foreach(BubbleDataType dialog in data.Dialogue){
            Dialogues.Enqueue(dialog);
        }
        bubbleBox.SetActive(true);
        offset = data.yOffset;
        isDialogueOpen = true;
        iterateDialogue(Dialogues.Dequeue());
    }

    public void iterateDialogue(BubbleDataType data){
        GameObject points =  GameObject.Find(data.Tag);
        Vector3 newPosition = new Vector3(points.transform.position.x, points.transform.position.y, 0);
        transform.position = newPosition;
        foreach(string text in data.text){
            dialogueText.Enqueue(text);
        }
        showNextDialogue();
    }

    public void showNextDialogue(){
        if(dialogueText.Count == 0){
            nextDialogue();
            return;
        }
        StopAllCoroutines();
        string sentence = dialogueText.Dequeue();
        Text.SetText(sentence);
        Text.ForceMeshUpdate();
        Vector2 textSize = Text.GetRenderedValues(false);
        Vector2 padding = new Vector2(1f, 1f);
        bgRenderer.size = textSize + padding;
        Text.SetText("");
        StartCoroutine(typeText(sentence));
    }

    IEnumerator typeText(string text){
        foreach(char letter in text.ToCharArray()){
            Text.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
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
        //End Dialogue
    }
}
