using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D collide;
    private bool canBeTriggered = false;

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
            GetComponent<Dialogue>().triggerDialogue();
        }
    }
}
