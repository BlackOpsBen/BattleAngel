using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBehavior : MonoBehaviour, IHurt
{
    [SerializeField] private string hurtSound;
    public void Hurt()
    {
        AudioManager.Instance.PlaySound(hurtSound, transform);
    }
}
