using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerTimeline : MonoBehaviour
{
    public PlayableDirector timeline;
    
    public void trigger(){
        timeline.Play();
    }
    

    public void pause(){
        timeline.Pause();
    }

    public void stop(){
        timeline.Stop();
    }
}
