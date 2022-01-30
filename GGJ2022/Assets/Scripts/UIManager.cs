using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject mainMenuUI;
    bool isPaused = false;
    bool onMainMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        onMainMenu = true;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (onMainMenu == true)
        {

        }
        else
        {
            Pause();
        }
    }

    public void PlayGame()
    {
        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(false);
        onMainMenu = false;
        Time.timeScale = 1;
    }
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;

    }
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
