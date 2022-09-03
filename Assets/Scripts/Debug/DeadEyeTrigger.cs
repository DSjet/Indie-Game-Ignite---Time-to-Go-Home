using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeadEyeTrigger : MonoBehaviour
{
    public static bool hasBeenTriggered = false;
    public UnityEvent ev;
    public float coordinateX;
    public PlayerMovement pm;
    private void OnTriggerEnter2D(Collider2D other){
        hasBeenTriggered = true;
        Vector2 pos = new Vector2(coordinateX, -0.5f);
        if(other.gameObject.tag == "Player"){
            ev?.Invoke();
        }
    }
}
