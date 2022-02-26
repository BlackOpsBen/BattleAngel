using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonnaCryDialog : MonoBehaviour
{
    public bool hasPlayed;

    public string soundName;

    public void PlaySound()
    {
        if (!hasPlayed)
        {
            hasPlayed = AudioManager.Instance.PlayDialog(AudioManager.PLAYERNAME, soundName, INTERRUPT_MODE: AudioManager.INTERRUPT_OVERLAP);
        }
    }
}
