using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefaultDeathBehavior : MonoBehaviour, IDie
{
    [SerializeField] private GameObject[] livingObjects;
    [SerializeField] private GameObject[] deadObjects;
    [SerializeField] private Collider[] collidersToDisable;
    [SerializeField] private MonoBehaviour[] scriptsToDisable;

    [SerializeField] private float explosiveForce = 1f;
    [SerializeField] private float explosiveRadius = 5f;

    public virtual void Start()
    {
        foreach (GameObject deadObject in deadObjects)
        {
            deadObject.SetActive(false);
        }
    }

    public virtual void Die()
    {
        foreach (GameObject livingObject in livingObjects)
        {
            livingObject.SetActive(false);
        }

        foreach (GameObject deadObject in deadObjects)
        {
            deadObject.SetActive(true);
        }

        Animator animator;
        if (animator = GetComponent<Animator>())
        {
            animator.enabled = false;
        }
        else if (animator = GetComponentInChildren<Animator>())
        {
            animator.enabled = false;
        }
        else
        {
            Debug.LogWarning("No animator found in " + gameObject.name);
        }

        NavMeshAgent navMeshAgent;
        if (navMeshAgent = GetComponent<NavMeshAgent>())
        {
            navMeshAgent.isStopped = true;
        }

        foreach (Collider mCollider in collidersToDisable)
        {
            mCollider.enabled = false;
        }

        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false;
        }
        /*if (collidersToDisable != null)
        {
            collidersToDisable.enabled = false;
        }*/

        GameObject deadBodiesParent = new GameObject("[" + gameObject.name + "_DEAD]");

        for (int i = 0; i < deadObjects.Length; i++)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = deadObjects[i].GetComponent<SkinnedMeshRenderer>();
            Mesh bakedMesh = new Mesh();
            skinnedMeshRenderer.BakeMesh(bakedMesh);

            MeshRenderer meshRenderer = deadObjects[i].AddComponent<MeshRenderer>();
            MeshFilter meshFilter = deadObjects[i].AddComponent<MeshFilter>();

            meshFilter.mesh = bakedMesh;
            meshRenderer.materials = skinnedMeshRenderer.materials;

            skinnedMeshRenderer.enabled = false;

            //deadObjects[i].transform.SetParent(deadBodiesParent.transform);

            Rigidbody rb = deadObjects[i].GetComponent<Rigidbody>();
            MeshCollider meshCollider = deadObjects[i].AddComponent<MeshCollider>();
            meshCollider.sharedMesh = bakedMesh;
            meshCollider.convex = true;

            rb.AddExplosionForce(explosiveForce, transform.position, explosiveRadius);

            deadObjects[i].AddComponent<RemoveRigidBody>();
        }

        AudioManager.Instance.PlaySound("SC_EnemyDeath");

        RemoveFromMiniMap();
    }

    private void RemoveFromMiniMap()
    {
        AppearOnMap appearOnMap;
        if (appearOnMap = GetComponent<AppearOnMap>())
        {
            MiniMap.Instance.RemoveItemFromMap(appearOnMap);
        }
        else
        {
            Debug.LogWarning("This was not on the minimap.");
        }
    }
}
