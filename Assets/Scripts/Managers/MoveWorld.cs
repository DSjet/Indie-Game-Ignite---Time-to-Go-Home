using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveWorld : MonoBehaviour
{
    public FadeAnimator fadescript;
    public void MoveScene(string SceneName){
        GameObject.Find("Sound Systems").GetComponent<SoundManager>().StopAllSound();
        fadescript.triggerFadeout(SceneName);
    }

    public void MoveScene(int index){
        GameObject.Find("Sound Systems").GetComponent<SoundManager>().StopAllSound();
        fadescript.triggerFadeout(index);
    }
}
