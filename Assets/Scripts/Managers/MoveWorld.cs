using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveWorld : MonoBehaviour
{
    public void MoveScene(string SceneName){
        SceneManager.LoadScene(SceneName);
    }

    public void MoveScene(int index){
        SceneManager.LoadScene(index);
    }
}
