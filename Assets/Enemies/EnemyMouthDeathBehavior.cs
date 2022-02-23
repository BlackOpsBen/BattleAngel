using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouthDeathBehavior : DefaultDeathBehavior
{
    [SerializeField] private Transform target;

    public override void Die()
    {
        target.tag = "Enemy";

        base.Die();
    }
}
