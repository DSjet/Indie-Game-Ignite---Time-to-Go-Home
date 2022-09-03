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
        Time.timeScale = 0;
    }

    public void OnResumeButtonClick(){
        pauseMenu.SetActive(false);
        worldUI.SetActive(true);
        Time.timeScale = 1;
    }

    public void OnExitButtonClick(){
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
