using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopUp : MonoBehaviour
{
    public GameObject PopUpBalloon;

    public void showPopUp(bool state){
        PopUpBalloon.SetActive(state);
    }
}
