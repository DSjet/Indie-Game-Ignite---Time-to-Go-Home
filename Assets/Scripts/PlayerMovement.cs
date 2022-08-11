using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 startTouchPosition;
    Vector2 currentPosition;
    Vector2 endTouchPosition;
    bool stopTouch = false;

    [SerializeField] float swipeRange;
    [SerializeField ] float tapRange;
    // Update is called once per frame
    void Update()
    {
        Swipe();
    }

    void Swipe ()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 distance = currentPosition - startTouchPosition;

            if (!stopTouch)
            {
                if (distance.x < -swipeRange)
                {
                    transform.position = new Vector3 (transform.position.x - 1, transform.position.y, 0);
                    stopTouch = true;
                }
                else if (distance.x > swipeRange)
                {
                    transform.position = new Vector3 (transform.position.x + 1, transform.position.y, 0);
                    stopTouch = true;
                }
                else if (distance.y > swipeRange)
                {
                    transform.position = new Vector3 (transform.position.x, transform.position.y + 1, 0);
                    stopTouch = true;
                }
                else if (distance.y < -swipeRange)
                {
                    transform.position = new Vector3 (transform.position.x, transform.position.y - 1, 0);
                    stopTouch = true;
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;

            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {
                // Tap Behaviour
                if(FindObjectOfType<DialogueSystems>().isOpen){
                    FindObjectOfType<DialogueSystems>().nextSentences();
                }
            }
        }
    }
}
