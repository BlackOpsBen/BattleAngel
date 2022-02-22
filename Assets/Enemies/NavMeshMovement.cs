using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : MonoBehaviour, IToggleWhenRevealed
{
    private NavMeshAgent navMeshAgent;

    public Transform targetPlayer;

    private bool isActive = true;

    private void Awake()
    {
        targetPlayer = FindObjectOfType<Movement>().transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isActive)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = targetPlayer.position;
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
    }

    public Transform GetTargetPlayer()
    {
        return targetPlayer;
    }

    public void ToggleActive(bool active)
    {
        isActive = active;
    }
}
