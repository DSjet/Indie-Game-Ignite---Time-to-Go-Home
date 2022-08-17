using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDialogue : MonoBehaviour
{
    public BubbleDataSO Data;


    public void triggerDialogue(){
        FindObjectOfType<BubbleSystems>().startDialogue(Data);
    }
}
