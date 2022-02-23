using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour
{
    [SerializeField] private Transform defaultTarget;
    [SerializeField] private float maxAimDegrees = 10.0f;
    [SerializeField] private Transform muzzle;

    private float halfArc;

    public List<Transform> allTargets = new List<Transform>();

    public List<Transform> arcTargets = new List<Transform>();

    public Transform actualTarget;

    private void Start()
    {
        halfArc = maxAimDegrees / 2;
    }

    private void Update()
    {
        FindAllTargets();
        FindTargetsInArc();
        //RemoveBlockedTargets();

        if (arcTargets.Count > 0)
        {
            actualTarget = FindClosestTarget();
        }
        else
        {
            actualTarget = defaultTarget;
        }
    }

    private void FindAllTargets()
    {
        allTargets.Clear();
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in targets)
        {
            allTargets.Add(target.transform);
        }
    }

    private void FindTargetsInArc()
    {
        arcTargets = new List<Transform>();
        for (int i = 0; i < allTargets.Count; i++)
        {
            if (CheckIsInFiringArc(allTargets[i]))
            {
                arcTargets.Add(allTargets[i]);
            }
        }
    }

    private bool CheckIsInFiringArc(Transform target)
    {
        Vector3 a = target.position - transform.position;
        Vector3 b = transform.forward;

        float angle = Vector3.Angle(a, b);

        if (angle > halfArc)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void RemoveBlockedTargets()
    {
        List<Transform> toRemove = new List<Transform>();

        foreach (Transform target in arcTargets)
        {
            RaycastHit hit;
            Physics.Raycast(muzzle.position, target.position - muzzle.position, out hit);

            if (hit.collider != null && !hit.collider.CompareTag("Enemy"))
            {
                toRemove.Add(target);
            }
        }
        foreach (Transform targetToRemove in toRemove)
        {
            arcTargets.Remove(targetToRemove);
        }
    }

    private Transform FindClosestTarget()
    {
        Transform closestTarget = arcTargets[0];
        float closestTargetDist = Vector3.Distance(transform.position, closestTarget.position);

        for (int i = 1; i < arcTargets.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, arcTargets[i].position);
            if (distance < closestTargetDist)
            {
                closestTarget = arcTargets[i];
                closestTargetDist = distance;
            }
        }
        return closestTarget;
    }

    public Transform GetTarget()
    {
        return actualTarget;
    }
}
