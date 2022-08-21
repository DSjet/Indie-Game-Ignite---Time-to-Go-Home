using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rb;
    private bool disableMovement = false;
    public bool doneWaitForMove = true;

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

    void FixedUpdate(){
        if(!disableMovement && doneWaitForMove){
            movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                if(Mathf.Abs(movement.x) > 0){
                    movement.y = 0;
                }
                if(Mathf.Abs(movement.y) > 0){
                    movement.x = 0;
                }
            if(movement.x != movement.y){
                Vector2 playerPoint = new Vector2(transform.position.x, transform.position.y);
                Vector2 newPoint = playerPoint + movement;
                StartCoroutine(handleWaitTimeMovement(newPoint, playerPoint));
            }
        }

    }

    public void disableInputState(bool state){
        disableMovement = state;
    }

    IEnumerator handleWaitTimeMovement(Vector2 pos, Vector2 start){
        doneWaitForMove = false;
        Vector2 isStuckPosition = start;
        int isStuck = 0;
        while((pos - rb.position).sqrMagnitude > 0.0025f){
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            yield return null;
            if(rb.position == isStuckPosition){
                if(isStuck > 3){
                    transform.position = start;
                    doneWaitForMove = true;
                    break;
                }
                isStuck++;
            }else{
                isStuckPosition = rb.position;
                isStuck = 0;
            }
        }
        if(isStuck == 0){
            transform.position = pos;
        }
        yield return null;
        doneWaitForMove = true;
    }
}
