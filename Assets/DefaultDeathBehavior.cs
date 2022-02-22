using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefaultDeathBehavior : MonoBehaviour, IDie
{
    [SerializeField] private GameObject[] livingObjects;
    [SerializeField] private GameObject[] deadObjects;
    //[SerializeField] private MonoBehaviour[] scriptsToDisable;

    [SerializeField] private float explosiveForce = 1f;
    [SerializeField] private float explosiveRadius = 5f;

    private void Start()
    {
        foreach (GameObject deadObject in deadObjects)
        {
            deadObject.SetActive(false);
        }
    }

    public void Die()
    {
        Debug.LogWarning("Player is dead!");

        foreach (GameObject livingObject in livingObjects)
        {
            livingObject.SetActive(false);
        }

        foreach (GameObject deadObject in deadObjects)
        {
            deadObject.SetActive(true);
        }

        /*foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false;
        }*/

        Animator animator;
        if (animator = GetComponent<Animator>())
        {
            animator.enabled = false;
        }
        else if (animator = GetComponentInChildren<Animator>())
        {
            animator.enabled = false;
        }
        //GetComponent<Animator>().enabled = false;
        NavMeshAgent navMeshAgent;
        if (navMeshAgent = GetComponent<NavMeshAgent>())
        {
            navMeshAgent.isStopped = true;
        }
        //GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<Collider>().enabled = false;

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

            deadObjects[i].transform.SetParent(deadBodiesParent.transform);

            Rigidbody rb = deadObjects[i].GetComponent<Rigidbody>();
            MeshCollider meshCollider = deadObjects[i].AddComponent<MeshCollider>();
            meshCollider.sharedMesh = bakedMesh;
            meshCollider.convex = true;

            rb.AddExplosionForce(explosiveForce, transform.position, explosiveRadius);

            deadObjects[i].AddComponent<RemoveRigidBody>();
        }
    }
}
