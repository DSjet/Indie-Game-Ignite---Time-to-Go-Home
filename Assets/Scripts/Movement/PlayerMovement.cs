using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    private bool disableMovement = false;

    Vector2 movement;

    void Awake(){
        GameManager.OnGameStateChanged += GameManagerOnStateChange;
    }

    void OnDestroy(){
        GameManager.OnGameStateChanged -= GameManagerOnStateChange;
    }

    private void GameManagerOnStateChange(GameManager.GameState state){
        disableMovement = (state == GameManager.GameState.CUTSCENESTATE);
    }

    void Start(){
       rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(!disableMovement){
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
    }

    public void disableInputState(bool state){
        disableMovement = state;
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
