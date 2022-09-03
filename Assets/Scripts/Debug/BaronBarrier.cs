using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaronBarrier : MonoBehaviour
{
    public static int gotTriggered = 0;
    public float coordinateY;
    public Transform target;
    public UnityEvent invokeWhenTriggeredThrice;
    private PlayerMovement pm;

    void Start(){
        pm  = target.GetComponent<PlayerMovement>();
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            gotTriggered++;
            Vector2 pos = new Vector2(target.position.x, coordinateY);
            target.position = pos;
            if(gotTriggered > 2){
                invokeWhenTriggeredThrice?.Invoke();
            }
        }
    }
}
