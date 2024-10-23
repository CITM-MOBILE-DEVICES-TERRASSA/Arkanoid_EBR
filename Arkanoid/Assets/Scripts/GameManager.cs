using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private ScoreManager scoreManager;
    private SaveManager saveManager;

    public enum GameState { Menu, Playing, Paused, GameOver, LevelComplete }
    public GameState currentState;
    private GameState auxState;
    private bool isGamePaused = false;

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
        Debug.Log("Entering SaveGame");
        Debug.Log(ScoreManager.Instance.GetScore());
        Debug.Log(ScoreManager.Instance.GetBestScore());
        GameData data = new GameData
        {
            score = ScoreManager.Instance.GetScore(),
            bestScore = ScoreManager.Instance.GetBestScore()
            //playerLives = HeartManager.Instance.GetLives(),
            //playerPositionX = Player.Instance.transform.position.x,
            //playerPositionY = Player.Instance.transform.position.y,
            //currentLevel = SceneManager.GetActiveScene().name
        };

        Debug.Log("Score: " + data.score);
        Debug.Log("Score: " + data.bestScore);

        SaveManager.Instance.SaveGame(data); // Guarda todos los datos usando SaveManager
    }

    public void LoadGame()
    {
        GameData data = SaveManager.Instance.LoadGame(); // Cargar datos del SaveManager
        if (data != null)
        {
            ScoreManager.Instance.SetScore(data.score);
            ScoreManager.Instance.SetBestScore(data.bestScore);
            //HeartManager.Instance.SetLives(data.playerLives);
            //Player.Instance.transform.position = new Vector3(data.playerPositionX, data.playerPositionY, 0);
            //SceneManager.LoadScene(data.currentLevel);
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Calling SaveGame");
        SaveGame(); // Guardar automáticamente al salir
        Debug.Log("SaveGame Finished");
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
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("Level 1");  // Reinicia al primer nivel
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level 1");
        currentState = GameState.Playing;
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        SceneManager.LoadScene("Menu");
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

