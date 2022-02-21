using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Sound Cue", menuName = "Audio/Sound Cue")]
public class SoundCue : ScriptableObject
{
    [Tooltip("A random clip will be played from this list whenever this SoundCue is played by the AudioManager.")]
    [SerializeField] private AudioClip[] clipOptions;

    [Header("Audio Mixing"), Tooltip("Optional: The AudioMixerGroup through which to play this sound.")]
    [SerializeField] private AudioMixerGroup audioMixerGroup;

    [Header("Modulation"), Tooltip("Play sound with random pitch and volume.")]
    [SerializeField] private bool enableModulation = true;
    [SerializeField] private float pitchMin = 0.95f;
    [SerializeField] private float pitchMax = 1.05f;
    [SerializeField] private float volumeMin = 0.95f;
    [SerializeField] private float volumeMax = 1.05f;

    public AudioClip GetSpecificClip(int index)
    {
        return clipOptions[index];
    }

    public AudioClip GetRandomClip()
    {
        int rand = UnityEngine.Random.Range(0, clipOptions.Length);

        return clipOptions[rand];
    }

    public AudioMixerGroup GetAudioMixerGroup()
    {
        return audioMixerGroup;
    }

    public float GetPitch()
    {
        if (enableModulation)
        {
            return UnityEngine.Random.Range(pitchMin, pitchMax);
        }
        else
        {
            return 1.0f;
        }
    }

    public float GetVolume()
    {
        if (enableModulation)
        {
            return UnityEngine.Random.Range(volumeMin, volumeMax);
        }
        else
        {
            return 1.0f;
        }
    }
}
