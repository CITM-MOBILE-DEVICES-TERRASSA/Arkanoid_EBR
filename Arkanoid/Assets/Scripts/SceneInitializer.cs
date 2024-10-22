using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance != null)
        {
            //GameManager.Instance.OnSceneLoaded();  // Llamar a OnSceneLoaded del GameManager cuando la escena carga
        }
    }
}
