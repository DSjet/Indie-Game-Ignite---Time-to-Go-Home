using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D collide;
    private bool canBeTriggered = false;
    public UnityEvent events;

    void Start(){
        collide = GetComponentInChildren<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            FindObjectOfType<ShowPopUp>().showPopUp(true);
            canBeTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            FindObjectOfType<ShowPopUp>().showPopUp(false);
            canBeTriggered = false;
        }
    }

    void Update(){
        if(canBeTriggered && Input.GetKeyDown(KeyCode.F)){
            events?.Invoke();
        }
    }

    public void DeleteAfterDone(Collider2D collide){
        canBeTriggered = false;
        FindObjectOfType<ShowPopUp>().showPopUp(false);
        Destroy(this);
        if(collide != null) Destroy(collide);
    }
}
