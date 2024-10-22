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
            scoreManager = GetComponent<ScoreManager>();
            saveManager = GetComponent<SaveManager>();
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
            score = scoreManager.GetScore(), // Asegúrate de tener un método GetScore en ScoreManager
            bestScore = scoreManager.GetBestScore(), // Método para obtener el mejor puntaje
            //playerLives = HeartManager.Instance.GetLives(), // Suponiendo que tienes un método para obtener vidas
            //playerPositionX = Player.Instance.transform.position.x, // Suponiendo que tienes una referencia al jugador
            //playerPositionY = Player.Instance.transform.position.y,
            currentLevel = SceneManager.GetActiveScene().name
        };

        Debug.Log(data);

        saveManager.SaveGame(data); // Guarda todos los datos usando SaveManager
    }

    public void LoadGame()
    {
        GameData data = saveManager.LoadGame(); // Cargar datos del SaveManager
        if (data != null)
        {
            scoreManager.SetScore(data.score); // Método para establecer la puntuación
            scoreManager.SetBestScore(data.bestScore); // Método para establecer el mejor puntaje
            //HeartManager.Instance.SetLives(data.playerLives); // Método para establecer vidas
            //Player.Instance.transform.position = new Vector3(data.playerPositionX, data.playerPositionY, 0); // Mover al jugador a la posición guardada
            //SceneManager.LoadScene(data.currentLevel); // Cargar la escena guardada
        }
    }

    private void OnApplicationQuit()
    {
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
            Time.timeScale = 0;
        }
        else
        {
            isGamePaused = false;
            currentState = auxState;
            Time.timeScale = 1;
        }

        UpdateUI();
        Debug.Log("Game Paused");
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

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnSceneLoaded();
        }

        if (HeartManager.Instance != null)
        {
            HeartManager.Instance.OnSceneLoaded();
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.OnSceneLoaded();
        }
    }
}

