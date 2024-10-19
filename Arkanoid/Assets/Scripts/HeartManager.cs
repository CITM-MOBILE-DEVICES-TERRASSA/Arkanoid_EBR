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
            Destroy(heartIcons[heartsLeft]);
        }
    }
}
