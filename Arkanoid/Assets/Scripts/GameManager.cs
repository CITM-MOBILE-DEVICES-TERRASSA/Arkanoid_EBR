using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int bricksLeft;

    public static GameManager Instance;                                                     // Crear al GameManager como singletone para evitar tener varios

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        bricksLeft = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    public void BlockDestroyed()
    {
        bricksLeft--;
        if(bricksLeft <= 0)
        {
            LoadNextLevel();                                                                // Si no quedan bloques carga el siguiente nivel
        }
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);           // Carga el siguiente nivel
        }
        else SceneManager.LoadScene("Level 1");                                             // Si no hay mas niveles, vuelve al nivel inicial
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Level 1");
    }
}
