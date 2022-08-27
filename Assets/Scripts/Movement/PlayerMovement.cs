using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;

    Vector2 movement;

    public event Action OnEncountered;

    void Start(){
       rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void CheckForEncounters(){
        if (Physics2D.OverlapCircle(transform.position, 0.2f) != null){
            if (UnityEngine.Random.Range(1, 101) <= 10){
                OnEncountered();
            }
        }
    }
}
