using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;
    public GameObject pauseButton;
    public GameObject newGameButton;
    public GameObject continueButton;

    public bool isPauseActive = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // No destruir el UIManager al cambiar de escena
        }
    }

    public void HideAllPanels()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (levelCompletePanel != null) levelCompletePanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        HideAllPanels();
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    public void ToglePauseMenu()
    {
        Debug.Log("Game Paused: " + GameManager.Instance.isGamePaused);
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(GameManager.Instance.isGamePaused);
            Debug.Log(pauseMenuPanel);
            Time.timeScale = GameManager.Instance.isGamePaused ? 0 : 1; // Pausar/Reanudar el tiempo
        }
    }

    public void ShowGameOver()
    {
        HideAllPanels();
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        StartCoroutine(GameOverDelay());
    }

    public void ShowLevelComplete()
    {
        HideAllPanels();
        StartCoroutine(LevelCompleteDelay());
    }

    // M�todo para encontrar los paneles autom�ticamente despu�s de que la escena haya sido cargada
    public void OnSceneLoaded()
    {
        StartCoroutine(InitializeUIAfterSceneLoad()); // Esperar un frame para asegurarse de que la escena est� completamente cargada
    }

    private IEnumerator InitializeUIAfterSceneLoad()
    {
        yield return null;  // Espera un frame antes de intentar buscar los paneles

        pauseMenuPanel = GameObject.Find("PauseMenuPanel");
        gameOverPanel = GameObject.Find("GameOverPanel");
        levelCompletePanel = GameObject.Find("LevelCompletePanel");
        pauseButton = GameObject.Find("PauseButton");
        newGameButton = GameObject.Find("NewGameButton");
        continueButton = GameObject.Find("ContinueButton");

        // Asignar el bot�n de pausa al m�todo del GameManager
        if (pauseButton != null)
        {
            Debug.Log("Pause Button Found, assigning function...");
            Button btn = pauseButton.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(GameManager.Instance.ToglePause);  // Asignar la funcion
            }
        }

        // Asignar el bot�n de NewGame al m�todo del GameManager
        if (newGameButton != null)
        {
            Debug.Log("New Game Button Found, assigning function...");
            Button btn = newGameButton.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(GameManager.Instance.NewGame);  // Asigna el m�todo NewGame
            }
        }

        // Asignar el bot�n de Continue al m�todo del GameManager
        if (continueButton != null)
        {
            Debug.Log("Continue Button Found, assigning function...");
            Button btn = continueButton.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(GameManager.Instance.Continue);  // Asigna el m�todo Continue
            }
        }

        // Asegurarse de que los paneles est�n ocultos cuando se carga la escena
        HideAllPanels();
    }
    private IEnumerator GameOverDelay()
    {
        Debug.Log("!!!!!! Delay !!!!!!! -----> 2 seconds left");
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Menu");
        GameManager.Instance.currentState = GameManager.GameState.Menu;
    }

    private IEnumerator LevelCompleteDelay()
    {
        Debug.Log("!!!!!! Delay !!!!!!! -----> 2 seconds left");
        yield return new WaitForSeconds(2f);

        if (GameManager.Instance.nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(GameManager.Instance.nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("Level 1");  // Reinicia al primer nivel
        }
        GameManager.Instance.currentState = GameManager.GameState.Playing;
    }
}
