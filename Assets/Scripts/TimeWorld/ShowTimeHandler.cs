using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTimeHandler : MonoBehaviour
{
    public GameObject Timebox;
    void Start(){
        //Debug
        if(TimeWorld.isShowed){
            Timebox.SetActive(true);
        }else{
            TimeWorld.TimeSecond = 10;
            TimeWorld.pauseTimer();
            Timebox.SetActive(false);
            TimeWorld.isShowed = true;
        }
    }
}
