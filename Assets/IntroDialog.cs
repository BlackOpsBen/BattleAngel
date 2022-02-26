using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class IntroDialog : MonoBehaviour
{
    private PlayMusic playMusic;

    // Start is called before the first frame update
    void Start()
    {
        playMusic = FindObjectOfType<PlayMusic>();
        StartCoroutine(IntroDialogSequence());
    }

    private IEnumerator IntroDialogSequence()
    {
        playMusic.LowerVolume();
        yield return new WaitForSeconds(1.0f);
        AudioManager.Instance.PlayDialog(AudioManager.SUPPORTNAME, "SC_IntroSupport", INTERRUPT_MODE: AudioManager.INTERRUPT_SELF);
        yield return new WaitForSeconds(9.0f);
        AudioManager.Instance.PlayDialog(AudioManager.PLAYERNAME, "SC_YouGotIt", INTERRUPT_MODE: AudioManager.INTERRUPT_SELF);
        yield return new WaitForSeconds(2.0f);
        playMusic.RestoreVolume();
    }
}
