using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBehavior : MonoBehaviour, IHurt
{
    public void Hurt()
    {
        Debug.Log(gameObject.name + " was hurt!");
    }
}
