using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessHitPFX : MonoBehaviour
{
    [SerializeField] private string soundName;

    private void Start()
    {
        AudioManager.Instance.PlaySound(soundName, transform);
    }
}
