using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    public DialogueData[] Data;

    public bool triggerAfterDone = false;
    public UnityEvent ev;

    public void triggerDialogue(){
        FindObjectOfType<DialogueSystems>().startDialogue(Data, triggerAfterDone, ev);
    }
}
