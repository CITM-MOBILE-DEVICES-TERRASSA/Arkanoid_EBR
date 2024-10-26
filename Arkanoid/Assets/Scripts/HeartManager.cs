using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HeartManager : MonoBehaviour
{
    public static HeartManager Instance;  // Singleton del HeartManager
    public GameObject[] heartIcons;       // Array de íconos de corazones

    public int heartsLeft = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Establecer el número de corazones restantes basado en el número de íconos activos
        heartsLeft = heartIcons.Length;
        UpdateHeartIcons(); // Asegurarse de que los íconos se muestren correctamente al inicio
    }

    public void LoseHeart()
    {
        if (heartsLeft > 0)
        {
            heartsLeft--;
            heartIcons[heartsLeft].SetActive(false); // Desactivar el último corazón
            Debug.Log("Hearts Left: " + heartsLeft);
        }
    }

    private void UpdateHeartIcons()
    {
        // Mostrar u ocultar los corazones según el número de vidas restantes
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].SetActive(i < heartsLeft); // Activa si i es menor que las vidas restantes
        }
    }

    public void OnSceneLoaded()
    {
        // Si necesitas actualizar los corazones al cargar una nueva escena
        heartIcons = new GameObject[]
        {
            GameObject.Find("Heart"),
            GameObject.Find("Heart (1)"),
            GameObject.Find("Heart (2)")
        };

        UpdateHeartIcons(); // Actualizar los íconos después de cargar la escena
    }

    public int GetHearts()
    {
        return heartsLeft;
    }

    public void SetHearts(int newHearts)
    {
        heartsLeft = newHearts; // Establece el puntaje actual
        UpdateHeartIcons(); // Actualiza el UI
    }
}
