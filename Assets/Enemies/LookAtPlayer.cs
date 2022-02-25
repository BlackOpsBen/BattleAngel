using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private float verticalOffset = 1.0f;

    private void Update()
    {
        Vector3 position = GameManager.Instance.GetPlayerInstance().transform.position + (Vector3.up * verticalOffset);
        transform.LookAt(position);
    }
}
