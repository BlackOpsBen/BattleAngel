using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private List<AudioSource> mainAudioSources = new List<AudioSource>();
    [SerializeField] private AudioSource finalBossMusic;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private float lowerAmount = 20.0f;
    private float maxLevel;

    private int currentTrack;

    void Start()
    {
        currentTrack = UnityEngine.Random.Range(0, mainAudioSources.Count);

        StartMusic();

        mixer.GetFloat("Music", out maxLevel);

        Debug.Log("Music volume: " + maxLevel);
    }

    private void Update()
    {
        if (!finalBossMusic.isPlaying && !MusicIsPlaying())
        {
            StartMusic();
        }
    }

    public void StartBossMusic()
    {
        foreach (AudioSource source in mainAudioSources)
        {
            source.Stop();
        }

        finalBossMusic.Play();
    }

    private void StartMusic()
    {
        currentTrack = (currentTrack + 1) % mainAudioSources.Count;

        mainAudioSources[currentTrack].Play();
    }

    private bool MusicIsPlaying()
    {
        return mainAudioSources[currentTrack].isPlaying;
    }

    public void LowerVolume()
    {
        mixer.SetFloat("Music", maxLevel - lowerAmount);
    }

   public void RestoreVolume()
    {
        mixer.SetFloat("Music", maxLevel);
    }
}
