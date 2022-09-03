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
        //
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

    public void forcedTransformMethod(Vector2 pos){
        isForcedTransform = true;
        disableInputState(true);
        StopAllCoroutines();
        doneWaitForMove = true;
        disableInputState(false);
        isForcedTransform = false;
    }

    IEnumerator handleWaitTimeMovement(Vector2 pos, Vector2 start){
        doneWaitForMove = false;
        Vector2 isStuckPosition = start;
        int isStuck = 0;
        while((pos - rb.position).sqrMagnitude > 0.0025f){
            if(isForcedTransform){
                doneWaitForMove = true;
                transform.position = forcedTransformPosition;
                StopCoroutine(handleWaitTimeMovement(pos, start));
                break;
            }else{
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
        }
        if(isStuck == 0){
            transform.position = pos;
        }
        yield return null;
        doneWaitForMove = true;
    }

    void CheckForEncounters(){
        if (Physics2D.OverlapCircle(transform.position, 0.2f) != null){
            if (UnityEngine.Random.Range(1, 101) <= 10){
                OnEncountered();
            }
        }
    }
}
