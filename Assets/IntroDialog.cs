using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDialog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroDialogSequence());
    }

    private IEnumerator IntroDialogSequence()
    {
        AudioManager.Instance.PlayDialog(AudioManager.SUPPORTNAME, "SC_IntroSupport", INTERRUPT_MODE: AudioManager.INTERRUPT_SELF);
        yield return new WaitForSeconds(9.0f);
        AudioManager.Instance.PlayDialog(AudioManager.PLAYERNAME, "SC_YouGotIt", INTERRUPT_MODE: AudioManager.INTERRUPT_SELF);
    }
}
