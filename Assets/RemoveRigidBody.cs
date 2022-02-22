using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRigidBody : MonoBehaviour
{
    public float movementThreshold = .01f;
    public float minDelay = 2f;
    private float timer = 0f;
    Rigidbody rb;
    private bool isDone = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > minDelay)
        {
            if (!isDone && rb.velocity.magnitude < movementThreshold)
            {
                Destroy(rb);
                Destroy(GetComponent<Collider>());
                //GetComponent<Collider>().isTrigger = true;
                isDone = true;
            }
        }
    }
}
