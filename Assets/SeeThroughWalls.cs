using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThroughWalls : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] private LayerMask layerMask;
    private float yOffset = 1.0f;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        Vector3 playerOffsetPos = GameManager.Instance.GetPlayerInstance().transform.position + (Vector3.up * yOffset);

        Vector3 direction = playerOffsetPos - cameraTransform.position;

        float distToPlayer = Vector3.Distance(playerOffsetPos, cameraTransform.position);

        RaycastHit[] hits = Physics.RaycastAll(cameraTransform.position, direction, distToPlayer, layerMask);

        Debug.DrawLine(cameraTransform.position, cameraTransform.position + direction, Color.red);

        foreach (RaycastHit hit in hits)
        {
            IsSeeThrough seeThrough;
            if (seeThrough = hit.collider.GetComponent<IsSeeThrough>())
            {
                seeThrough.FadeOut();
            }
        }
    }
}
