using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeAnimator : MonoBehaviour
{
    public Animator animator;
    public Canvas canvas;
    private int index = -1;
    private string nameScene;

    public void triggerFadeout(int index){
        animator.SetTrigger("MoveScene");
        this.index = index;
    }

    public void triggerFadeout(string sceneName){
        animator.SetTrigger("MoveScene");
        this.nameScene = sceneName;
    }

    public void moveAfterDone(){
        if(index == -1){
            SceneManager.LoadScene(nameScene);
        }else{
            SceneManager.LoadScene(index);
        }
    }
}
