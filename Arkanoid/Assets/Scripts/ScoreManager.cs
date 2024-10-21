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
            LoadBestScore();
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
            SaveBestScore();
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
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }
        else if (bestScoreText == null)
        {
            bestScoreText = GameObject.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
        }

        UpdateScoreText();
    }

    private void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save();
    }
}
