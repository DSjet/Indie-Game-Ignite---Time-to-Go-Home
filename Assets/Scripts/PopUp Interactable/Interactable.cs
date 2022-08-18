using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D collide;

    void Start(){
        collide = GetComponentInChildren<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            FindObjectOfType<ShowPopUp>().showPopUp(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            FindObjectOfType<ShowPopUp>().showPopUp(false);
        }
    }
}
