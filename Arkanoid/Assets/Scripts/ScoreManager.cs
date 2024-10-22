using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;                    // Crear al BrickManager como singletone para evitar tener varios

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    private int score = 0;
    private int bestScore;


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
    }

    public void AddScore(int points)
    {
        score += points;
        if(score >= bestScore)
        {
            bestScore = score;
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()                      // Actualizamos el texto de la puntuación en el UI
    {
        if (scoreText != null)
        {
            scoreText.text = " " + score;
        }
        else
        {
            Debug.LogWarning("ScoreText is not assigned");
        }

        if (bestScoreText != null)
        {
            bestScoreText.text = " " + bestScore;
        }
        else
        {
            Debug.LogWarning("BestScoreText is not assigned");
        }
    }

    public void OnSceneLoaded()
    {
        StartCoroutine(InitializeUIAfterSceneLoad());
    }

    private IEnumerator InitializeUIAfterSceneLoad()
    {
        // Espera un frame antes de intentar buscar los paneles
        yield return null;

        // Intenta encontrar el ScoreText
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        }

        // Intenta encontrar el BestScoreText
        if (bestScoreText == null)
        {
            bestScoreText = GameObject.Find("BestScoreText")?.GetComponent<TextMeshProUGUI>();
        }

        // Verifica si los textos han sido encontrados
        if (scoreText == null)
        {
            Debug.LogWarning("ScoreText is not assigned or found in the scene.");
        }

        if (bestScoreText == null)
        {
            Debug.LogWarning("BestScoreText is not assigned or found in the scene.");
        }

        // Actualiza la UI
        UpdateScoreText();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public void SetScore(int newScore)
    {
        score = newScore; // Establece el puntaje actual
        UpdateScoreText(); // Actualiza el UI
    }

    public void SetBestScore(int newBestScore)
    {
        bestScore = newBestScore; // Establece el mejor puntaje
        UpdateScoreText(); // Actualiza el UI
    }
}
