using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource buttonSource;
    [SerializeField] private AudioSource signingSource;
    [SerializeField] private AudioSource typingSource;

    [SerializeField] private AudioClip[] musicClips;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        ProjectManager.DevelopmentEvent.AddListener(PlayTyping);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!musicSource.isPlaying) { RandomMusic(); }
    }

    public void PlayBtn(bool signing = false)
    {
        if (!signing) buttonSource.Play();
        else signingSource.Play();
    }

    private void PlayTyping(bool play)
    {
        if (play) typingSource.Play();
        else typingSource.Stop();
    }

    private void RandomMusic()
    {
        musicSource.clip = musicClips[Random.Range(0, musicClips.Length)];
        musicSource.Play();
    }

    public void SetSound(float volume)
    {
        musicSource.volume = volume;
        buttonSource.volume = volume;
        signingSource.volume = volume;
        typingSource.volume = volume;
    }

    private void OnLevelWasLoaded(int level)
    {
        // PlayTyping(false); if level not main
    }
}