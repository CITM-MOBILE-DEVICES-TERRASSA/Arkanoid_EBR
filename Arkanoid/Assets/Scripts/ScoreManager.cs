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
        yield return null; // Espera un fotograma

        scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        bestScoreText = GameObject.Find("BestScoreText")?.GetComponent<TextMeshProUGUI>();

        if (scoreText == null)
        {
            Debug.LogWarning("ScoreText is not found in the scene.");
        }

        if (bestScoreText == null)
        {
            Debug.LogWarning("BestScoreText is not found in the scene.");
        }

        UpdateScoreText(); // Actualiza el texto al final
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
        //score = newScore;
        UpdateScoreText();
    }

    public void SetBestScore(int newBestScore)
    {
        bestScore = newBestScore;
        UpdateScoreText();
    }
}
