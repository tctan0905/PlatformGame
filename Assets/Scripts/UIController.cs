using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject btn_Pause;
    public GameObject pausePanel;
    public GameObject pauseMenu;
    public GameObject optionMenu;

    private void Start()
    {
        btn_Pause.SetActive(true);
        pausePanel.SetActive(false);
        pauseMenu.SetActive(false);
        optionMenu.SetActive(false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        btn_Pause.SetActive(false);
        pausePanel.SetActive(true);
        pauseMenu.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        btn_Pause.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void Option()
    {
        pauseMenu.SetActive(false);
        optionMenu.SetActive(true);
    }
    public void BackToPauseMenu()
    {
        pauseMenu.SetActive(true);
        optionMenu.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
