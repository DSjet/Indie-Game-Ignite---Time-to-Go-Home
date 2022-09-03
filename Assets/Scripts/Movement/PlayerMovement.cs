using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    private bool disableMovement = false;
    public bool doneWaitForMove = true;
    public bool isForcedTransform = false;
    public Vector2 forcedTransformPosition;

    Vector2 movement;

    public event Action OnEncountered;

    void Start(){
       rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
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

    void CheckForEncounters(){
        if (Physics2D.OverlapCircle(transform.position, 0.2f) != null){
            if (UnityEngine.Random.Range(1, 101) <= 10){
                OnEncountered();
            }
        }
    }
}
