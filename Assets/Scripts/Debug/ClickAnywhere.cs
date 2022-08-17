using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAnywhere : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(FindObjectOfType<BubbleSystems>().isDialogueOpen){
                FindObjectOfType<BubbleSystems>().showNextDialogue();
            }
        }
    }
}
