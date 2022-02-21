using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private Transform targetPlayer;

    private void Awake()
    {
        targetPlayer = FindObjectOfType<CharacterController>().transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navMeshAgent.destination = targetPlayer.position;
    }
}
