using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouthDeathBehavior : DefaultDeathBehavior
{
    [SerializeField] private List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();
    [SerializeField] private Transform target;

    public override void Die()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false;
        }

        target.tag = "Enemy";

        AudioManager.Instance.PlaySound("SC_EnemyDeath");

        base.Die();
    }
}
