using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton para el AudioManager

    [Header("Audio Sources")]
    public AudioSource musicSource; // Para la m�sica de fondo
    public AudioSource sfxSource;   // Para efectos de sonido (SFX)

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;   // M�sica de fondo
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
        PlayMusic(backgroundMusic); // Iniciar la m�sica de fondo
    }

    // M�todo para reproducir m�sica de fondo
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // M�todo para reproducir efectos de sonido
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null)
        {
            Debug.Log("Playing SFX: " + sfxSource);
            sfxSource.PlayOneShot(clip);
        }
    }

    // M�todo para reproducir un efecto de sonido desde un �ndice de la lista
    public void PlaySFX(int index)
    {
        if (index >= 0 && index < soundEffects.Length)
        {
            PlaySFX(soundEffects[index]);
        }
    }

    // M�todo para detener la m�sica
    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
}

