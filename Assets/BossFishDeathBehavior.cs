using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class BossFishDeathBehavior : DefaultDeathBehavior
{
    private string objeciveName = "???";

    [SerializeField] private PlayAllSubPFX deathPFX;
    [SerializeField] private ShakePreset shakePreset;

    public override void Start()
    {
        base.Start();

        GameManager.Instance.objectiveUI.NewObjective(objeciveName, "");
    }

    public override void Die()
    {
        deathPFX.PlayAll();

        GetComponent<Rigidbody>().isKinematic = true;

        Shaker.ShakeAll(shakePreset);

        base.Die();

        StartCoroutine(DefeatSequence());
    }

    public IEnumerator DefeatSequence()
    {
        GameManager.Instance.objectiveUI.GetObjective(objeciveName).MarkComplete();

        GameManager.Instance.GetComponent<TrackGameStats>().StopClock();

        yield return new WaitForSeconds(2.0f);

        AudioManager.Instance.PlayDialog(AudioManager.SUPPORTNAME, "SC_OutroSupport", INTERRUPT_MODE: AudioManager.INTERRUPT_SELF);

        yield return new WaitForSeconds(5.0f);

        Time.timeScale = 0.0f;
        // Show UI
        GameManager.Instance.GetComponent<ShowEndUI>().ShowVictoryScreen();
    }
}
