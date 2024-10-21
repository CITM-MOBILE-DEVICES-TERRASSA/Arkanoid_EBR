using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public static HeartManager Instance;                                                     // Crear al HeartManager como singletone para evitar tener varios

    public GameObject[] heartIcons;

    public int heartsLeft;


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
        heartsLeft = heartIcons.Length;
    }

    public void LoseHeart()
    {
        if(heartsLeft > 0)
        {
            heartsLeft--;
            heartIcons[heartsLeft].SetActive(false);

            Debug.Log("Hearts Left: " +  heartsLeft);
        }
    }

    private void UpdateHeartIcons()
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {

            if (i < heartsLeft)
            {
                heartIcons[i].SetActive(true);                      // Mostramos los corazones que quedan
            }
            else
            {
                heartIcons[i].SetActive(false);                     // Ocultamos los que ya no quedan
            }
        }

    }

    public void OnSceneLoaded()
    {
        heartIcons = new GameObject[]
        {
            GameObject.Find("Heart"),
            GameObject.Find("Heart (1)"),
            GameObject.Find("Heart (2)")
        };

        UpdateHeartIcons();
    }
}
