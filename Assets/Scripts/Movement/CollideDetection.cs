using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideDetection : MonoBehaviour
{
    public bool isTriggered = false;
    void OnTriggerEnter2D(Collider2D other){
        if(!other.isTrigger){
            isTriggered = true;
        }
    }

    void OnTriggerExit2D(){
        isTriggered = false;
    }
}
