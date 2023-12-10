using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject pauseMenuUI, BaseMenuUI, optionsMenu, tutorial;
    public Slider slider;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(GameIsPaused) 
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }
    }
    public void Resume() 
    {
        pauseMenuUI.SetActive(false);
        BaseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        BaseMenuUI.SetActive(true);
        optionsMenu.SetActive(false);
        tutorial.SetActive(false);
        tutorial.GetComponent<UIIndexing>().ResetIndexing();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void PauseOnly()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void ResumeOnly()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void BackToMainMenu() 
    {
        Debug.Log("Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void Tutorial() 
    {
        Debug.Log("Tutorial");
    }
    public void Options() 
    {
        BaseMenuUI.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void BacktoBasePauseMenu() 
    {
        pauseMenuUI.SetActive(true);
        BaseMenuUI.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
