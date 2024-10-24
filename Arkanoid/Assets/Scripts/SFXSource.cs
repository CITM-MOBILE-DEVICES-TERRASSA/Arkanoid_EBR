using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSource : MonoBehaviour
{
    public static SFXSource Instance;

    private void Awake()
    {
        // Check if there is already an instance of AudioManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy this instance if there's already one
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed when loading a new scene
        }
    }
}
