using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AfterScenePlayerPosition : MonoBehaviour
{
    public GameObject player;
    public GameObject baron;
    public GameObject baronBarrier;
    public GameObject deadeyeTrigger;
    public UnityEvent eventDeadEyeTimeline;

    public static bool BaronIsDone = false;
    void Start(){
        if(BaronBarrier.gotTriggered > 2){
            BaronIsDone = true;
            Vector2 pos = new Vector2(-5.5f, 25.5f);
            player.transform.position = pos;
        }
        if(BaronIsDone){
            Destroy(baron);
            Destroy(baronBarrier);
        }
        if(DeadEyeTrigger.hasBeenTriggered){
            Destroy(deadeyeTrigger);
            eventDeadEyeTimeline?.Invoke();
        }
    }
}
