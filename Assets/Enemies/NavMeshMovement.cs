using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : MonoBehaviour, IToggleWhenRevealed
{
    private NavMeshAgent navMeshAgent;

    private bool isActive = true;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isActive)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = GameManager.Instance.GetPlayerInstance().transform.position;
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
    }

    public void ToggleActive(bool active)
    {
        isActive = active;
    }
}
