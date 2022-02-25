using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSeeThrough : MonoBehaviour
{
    private float blend = 1.0f;

    private int direction = 1;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        blend += Time.deltaTime * direction;
        blend = Mathf.Clamp01(blend);

        meshRenderer.material.SetFloat("_AlphaBlend", blend);
    }

    private void LateUpdate()
    {
        direction = 1;
    }

    public void FadeOut()
    {
        direction = -1;
    }
}
