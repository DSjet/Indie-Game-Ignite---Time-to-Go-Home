using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject worldUI;

    public void OnPauseButtonClick(){
        pauseMenu.SetActive(true);
        worldUI.SetActive(false);
    }

    public void OnResumeButtonClick(){
        pauseMenu.SetActive(false);
        worldUI.SetActive(true);
    }

    public void OnExitButtonClick(){
        SceneManager.LoadScene(0);
    }
}
