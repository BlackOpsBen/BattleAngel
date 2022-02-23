using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    private Transform target;

    [SerializeField] private float lerpSpeed = 5f;

    private void Awake()
    {
        target = FindObjectOfType<Movement>().transform;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * lerpSpeed);
        }
    }
}
