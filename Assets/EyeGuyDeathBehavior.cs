using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeGuyDeathBehavior : DefaultDeathBehavior
{
    [SerializeField] private GameObject eyePrefab;
    [SerializeField] private Transform eyeSpawnTransform;
    [SerializeField] private List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();
    public override void Die()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false;
        }

        Instantiate(eyePrefab, eyeSpawnTransform.position, eyeSpawnTransform.rotation);

        base.Die();
    }
}
