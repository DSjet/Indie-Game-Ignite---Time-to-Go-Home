using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDialogue : MonoBehaviour
{
    public BubbleDataSO Data;
    public bool ContinueWithBattle;


    public void triggerDialogue(){
        FindObjectOfType<BubbleSystems>().startDialogue(Data, ContinueWithBattle);
    }
}
