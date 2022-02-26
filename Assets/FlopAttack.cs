using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlopAttack : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float maxTorque = 10.0f;
    [SerializeField] private float groundCheckRadius = 1.5f;
    [SerializeField] private LayerMask groundCheckLayerMask;

    private void Update()
    {
        if (rb.velocity.sqrMagnitude < float.Epsilon)
        {
            Jump(Vector3.up);
        }
    }

    public void OnFlopUpEvent()
    {
        Jump(transform.up);
    }

    public void OnFlopDownEvent()
    {
        Jump(transform.up * -1);
    }

    private void Jump(Vector3 direction)
    {
        float randX = UnityEngine.Random.Range(-0.1f, 0.1f);
        float randY = UnityEngine.Random.Range(-0.1f, 0.1f);
        float randZ = UnityEngine.Random.Range(-0.1f, 0.1f);
        Vector3 rand = new Vector3(randX, randY, randZ);

        if (GetIsTouchingGround())
        {
            rb.AddForce((direction + Vector3.up + rand).normalized * jumpForce, ForceMode.Impulse);

            float torqueX = UnityEngine.Random.Range(-maxTorque, maxTorque);
            float torqueY = UnityEngine.Random.Range(-maxTorque, maxTorque);
            float torqueZ = UnityEngine.Random.Range(-maxTorque, maxTorque);
            rb.AddTorque(new Vector3(torqueX, torqueY, torqueZ));
        }
    }

    private bool GetIsTouchingGround()
    {
        return Physics.CheckSphere(transform.position, groundCheckRadius, groundCheckLayerMask);
    }
}
