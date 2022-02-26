using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class BossFishDeathBehavior : DefaultDeathBehavior
{
    private string objeciveName = "???";

    [SerializeField] private PlayAllSubPFX deathPFX;
    [SerializeField] private ShakePreset shakePreset;

    private void Start()
    {
        GameManager.Instance.objectiveUI.NewObjective(objeciveName, "");
    }

    public override void Die()
    {
        deathPFX.PlayAll();

        Shaker.ShakeAll(shakePreset);

        base.Die();

        StartCoroutine(DefeatSequence());
    }

    public IEnumerator DefeatSequence()
    {
        GameManager.Instance.objectiveUI.GetObjective(objeciveName).MarkComplete();

        yield return new WaitForSeconds(2.0f);

        Debug.Log("Victory!");

        // TODO end game
    }
}
