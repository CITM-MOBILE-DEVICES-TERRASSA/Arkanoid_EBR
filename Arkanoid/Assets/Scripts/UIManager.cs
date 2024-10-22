using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;
    public GameObject pauseButton;

    private bool isPauseActive = false;

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
        isPauseActive = !isPauseActive;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(isPauseActive);
        Time.timeScale = isPauseActive ? 0 : 1;  // Pausar/Reanudar el tiempo
    }

    public void ShowGameOver()
    {
        HideAllPanels();
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void ShowLevelComplete()
    {
        HideAllPanels();
        if (levelCompletePanel != null) levelCompletePanel.SetActive(true);
    }

    // Método para encontrar los paneles automáticamente después de que la escena haya sido cargada
    public void OnSceneLoaded()
    {
        StartCoroutine(InitializeUIAfterSceneLoad()); // Esperar un frame para asegurarse de que la escena está completamente cargada
    }

    private IEnumerator InitializeUIAfterSceneLoad()
    {
        yield return null;  // Espera un frame antes de intentar buscar los paneles

        // Buscamos cada panel usando GameObject.Find para asignarlos automáticamente
        mainMenuPanel = GameObject.Find("MainMenuPanel");
        pauseMenuPanel = GameObject.Find("PauseMenuPanel");
        gameOverPanel = GameObject.Find("GameOverPanel");
        levelCompletePanel = GameObject.Find("LevelCompletePanel");
        pauseButton = GameObject.Find("PauseButton");

        // Asignar el botón de pausa al método del GameManager
        if (pauseButton != null)
        {
            Debug.Log("Pause Button Found, assigning function...");
            Button btn = pauseButton.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();  // Asegurarse de limpiar los oyentes anteriores
                btn.onClick.AddListener(GameManager.Instance.ToglePause);  // Asignar el método
            }
        }

        // Asegurarse de que los paneles están ocultos cuando se carga la escena
        HideAllPanels();
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
    }
}
