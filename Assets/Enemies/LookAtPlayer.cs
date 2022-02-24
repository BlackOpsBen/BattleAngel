using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float verticalOffset = 1.0f;

    private void Awake()
    {
        player = FindObjectOfType<Movement>().transform;
    }

    private void Update()
    {
        Vector3 position = player.position + (Vector3.up * verticalOffset);
        transform.LookAt(position);
    }
}
