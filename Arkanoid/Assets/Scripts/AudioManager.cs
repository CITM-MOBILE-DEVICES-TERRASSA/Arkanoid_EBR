using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton para el AudioManager

    [Header("Audio Source")]
    public AudioSource audioSource; // Para la música de fondo y los SFX

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;   // Música de fondo
    public AudioClip[] soundEffects;    // Lista de efectos de sonido

    private void Awake()
    {
        // Configurar el AudioManager como singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener el AudioManager al cambiar de escena
        }
    }

    private void Start()
    {
        PlayMusic(backgroundMusic); // Iniciar la música de fondo
    }

    // Método para reproducir música de fondo
    public void PlayMusic(AudioClip clip)
    {
        if (audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Método para reproducir efectos de sonido
    public void PlaySFX(AudioClip clip)
    {
        if (audioSource != null)
        {
            Debug.Log("Playing SFX: " + audioSource);
            audioSource.PlayOneShot(clip);
        }
    }

    // Método para reproducir un efecto de sonido desde un índice de la lista
    public void PlaySFX(int index)
    {
        if (index >= 0 && index < soundEffects.Length)
        {
            PlaySFX(soundEffects[index]);
        }
    }

    // Método para detener la música
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}

