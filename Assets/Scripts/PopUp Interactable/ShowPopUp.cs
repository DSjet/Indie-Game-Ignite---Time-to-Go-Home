using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopUp : MonoBehaviour
{
    public GameObject PopUpBalloon;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPopUp(bool state){
        PopUpBalloon.SetActive(state);
    }
}
