using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public DialogueData[] Data;


    public void triggerDialogue(){
        FindObjectOfType<DialogueSystems>().startDialogue(Data);
    }
}
