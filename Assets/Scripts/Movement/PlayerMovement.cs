using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    private bool disableMovement = false;

    Vector2 movement;

    void Start(){
       rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if (GameManager.Instance.getState() == GameState.FreeRoam){
            disableInputState(false);
        } 
        else{
            disableInputState(true);
        }
        if(!disableMovement){
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
    }

    void FixedUpdate(){
        if(!disableMovement)
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void disableInputState(bool state){
        disableMovement = state;
    }
}
