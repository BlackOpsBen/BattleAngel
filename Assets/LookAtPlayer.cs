using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player;

    private void Awake()
    {
        player = FindObjectOfType<Movement>().transform;
    }

    private void Update()
    {
        transform.LookAt(player);
    }
}
