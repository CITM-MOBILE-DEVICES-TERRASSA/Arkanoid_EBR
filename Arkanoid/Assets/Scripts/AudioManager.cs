using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton para el AudioManager

    [Header("Audio Sources")]
    public AudioSource musicSource; // Para la música de fondo
    public AudioSource sfxSource;   // Para efectos de sonido (SFX)

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
        if (musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // Método para reproducir efectos de sonido
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null)
        {
            Debug.Log("Playing SFX: " + sfxSource);
            sfxSource.PlayOneShot(clip);
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
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
}

