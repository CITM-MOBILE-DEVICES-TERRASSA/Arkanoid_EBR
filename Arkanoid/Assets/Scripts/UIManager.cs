using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;                    // Crear al UIManager como singletone para evitar tener varios


    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;
    public GameObject pauseButton;
    public GameObject resumeButton;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        OnSceneLoaded();
        ShowMainMenu();
    }

    public void HideAllPanels()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; 
    }

    public void ShowLevelComplete()
    {
        levelCompletePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void OnSceneLoaded()
    {
        mainMenuPanel = GameObject.Find("MainMenuPanel");
        pauseMenuPanel = GameObject.Find("PauseMenuPanel");
        gameOverPanel = GameObject.Find("GameOverPanel");
        levelCompletePanel = GameObject.Find("LevelCompletePanel");
        pauseButton = GameObject.Find("PauseButton");
        resumeButton = GameObject.Find("resumeButton");
    }
}
