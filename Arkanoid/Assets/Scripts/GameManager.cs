using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;                                                     // Crear al GameManager como singletone para evitar tener varios

    public enum GameState { Menu, Playing, Paused, GameOver}
    public GameState currentState;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            OnSceneLoaded();
        }
    }

    private void Start()
    {
        currentState = GameState.Menu;                                                      // Estado inicial
    }

    private void OnSceneLoaded()
    {
        if(ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnSceneLoaded();
        }

        if(HeartManager.Instance != null)
        {
            HeartManager.Instance.OnSceneLoaded();
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.OnSceneLoaded();
        }

        UpdateUI();
    }

    public void LoadNextLevel()
    {
        currentState = GameState.Playing;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);           // Carga el siguiente nivel
        }
        else SceneManager.LoadScene("Level 1");                                             // Si no hay mas niveles, vuelve al nivel inicial
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level 1");
        currentState = GameState.Playing;
        Time.timeScale = 1;
        UpdateUI();
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        UpdateUI();
    }

    public void PauseGame()
    {
        currentState = GameState.Paused;                                                    // Cambiar estado a Pausado
        Time.timeScale = 0;                                                                 // Pausar el tiempo
        UpdateUI();
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;                                                   // Cambiar estado a Jugando
        Time.timeScale = 1;                                                                 // Reanudar el tiempo
        UpdateUI();
    }

    private void UpdateUI()
    {
                                                                                            // Actualiza la UI según el estado del juego
        if (currentState == GameState.Menu)
        {
            UIManager.Instance.HideAllPanels();
            UIManager.Instance.ShowMainMenu();
        }
        else if (currentState == GameState.Playing)
        {
            UIManager.Instance.HideAllPanels();
            UIManager.Instance.ResumeGame();
        }
        else if (currentState == GameState.Paused)
        {
            UIManager.Instance.HideAllPanels();
            UIManager.Instance.ShowPauseMenu();
        }
        else if (currentState == GameState.GameOver)
        {
            UIManager.Instance.HideAllPanels();
            UIManager.Instance.ShowGameOver();
        }
    }
}
