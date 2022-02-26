using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();

    private int currentTrack;

    void Start()
    {
        currentTrack = UnityEngine.Random.Range(0, audioSources.Count);

        StartMusic();
    }

    private void Update()
    {
        if (!MusicIsPlaying())
        {
            StartMusic();
        }
    }

    private void StartMusic()
    {
        currentTrack = (currentTrack + 1) % audioSources.Count;

        audioSources[currentTrack].Play();
    }

    private bool MusicIsPlaying()
    {
        return audioSources[currentTrack].isPlaying;
    }
}
