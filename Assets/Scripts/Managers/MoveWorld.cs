using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveWorld : MonoBehaviour
{
    public FadeAnimator fadescript;
    public void MoveScene(string SceneName){
        fadescript.triggerFadeout(SceneName);
    }

    public void MoveScene(int index){
        fadescript.triggerFadeout(index);
    }
}
