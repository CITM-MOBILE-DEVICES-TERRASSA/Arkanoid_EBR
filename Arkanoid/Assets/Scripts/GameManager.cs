using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { Menu, Playing, Paused, GameOver, LevelComplete }
    public GameState currentState;
    private GameState auxState;
    public bool isGamePaused = false;
    public bool isNewGame = false;

    public int nextSceneIndex;


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
        LoadGame();
        currentState = GameState.Menu;
        UpdateUI();
    }

    public void SaveGame()
    {
        GameData data = new GameData
        {
            score = ScoreManager.Instance.GetScore(),
            bestScore = ScoreManager.Instance.GetBestScore(),
            playerLives = HeartManager.Instance.GetHearts(),
            currentLevel = nextSceneIndex
        };

        Debug.Log("---------- Game Saved! ---------");
        Debug.Log("Score Saved: " + data.score);
        Debug.Log("BestScore Saved: " + data.bestScore);
        Debug.Log("Player lives Saved: " + data.playerLives);
        Debug.Log("Level " + data.currentLevel + " Saved");
        Debug.Log("--------------------------------");

        SaveManager.Instance.SaveGame(data); // Guarda todos los datos usando SaveManager
    }

    public void LoadGame()
    {
        GameData data = SaveManager.Instance.LoadGame(); // Cargar datos del SaveManager
        if (data != null)
        {
            ScoreManager.Instance.SetScore(data.score);
            ScoreManager.Instance.SetBestScore(data.bestScore);
            HeartManager.Instance.SetHearts(data.playerLives);
            //SceneManager.LoadScene(data.currentLevel);
        }

        Debug.Log("---------- Game Loaded! ---------");
        Debug.Log("Score Loaded: " + data.score);
        Debug.Log("BestScore Loaded: " + data.bestScore);
        Debug.Log("Player lives Loaded: " + data.playerLives);
        Debug.Log("Level " + data.currentLevel + " Loaded");
        Debug.Log("---------------------------------");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Calling SaveGame");
        SaveGame(); // Guardar automáticamente al salir
    }

    // Se asegura de que OnSceneLoaded se llame al terminar de cargar una nueva escena
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Suscribir al evento de carga de escena
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desuscribir para evitar errores si el objeto se destruye
    }

    public void LoadNextLevel()
    {
        currentState = GameState.LevelComplete;
        SceneManager.LoadScene("LevelComplete");
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        SaveGame();
        UpdateUI();
    }

    public void NewGame()
    {
        currentState = GameState.Playing;
        SceneManager.LoadScene("Level 1");
        UpdateUI();
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        SceneManager.LoadScene("GameOver");
    }

    public void ToglePause()
    {
        if (!isGamePaused)
        {
            isGamePaused = true;
            auxState = currentState;
            currentState = GameState.Paused;
            Time.timeScale = 0; // Pausar el juego
        }
        else
        {
            isGamePaused = false;
            currentState = auxState;
            Time.timeScale = 1; // Reanudar el juego
        }

        UpdateUI();
        Debug.Log(isGamePaused ? "Game Paused" : "Game Resumed");
    }

    public void Menu()
    {
        SaveGame();
        currentState = GameState.Menu;
        SceneManager.LoadScene("Menu");
    }

    public void Continue()
    {
        LoadGame();
        currentState = GameState.Playing;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void UpdateUI()
    {
        if (UIManager.Instance != null)
        {
            switch (currentState)
            {
                case GameState.Menu:
                    UIManager.Instance.ShowMainMenu();
                    break;
                case GameState.Playing:
                    UIManager.Instance.HideAllPanels();
                    break;
                case GameState.Paused:
                    UIManager.Instance.ToglePauseMenu();
                    break;
                case GameState.GameOver:
                    UIManager.Instance.ShowGameOver();
                    break;
                case GameState.LevelComplete:
                    UIManager.Instance.ShowLevelComplete();
                    break;
            }
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        UpdateUI(); // Sincronizar UI con el nuevo estado del juego

        if (HeartManager.Instance != null)
        {
            HeartManager.Instance.OnSceneLoaded();
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.OnSceneLoaded();
        }

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnSceneLoaded();
        }
    }
}

