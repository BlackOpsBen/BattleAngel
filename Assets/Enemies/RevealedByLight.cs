using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealedByLight : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    private string propertyName = "_CutoffHeight";

    private Material material;

    private float minCutoffHeight = -0.45f;
    private float maxCutoffHeight = 4.83f;

    private float blend = 0.0f;

    [SerializeField] private float blendSpeed = 2.0f;

    private int direction = -1;

    private void Start()
    {
        material = skinnedMeshRenderer.material;
        material.SetFloat(propertyName, minCutoffHeight);
        //skinnedMeshRenderer.enabled = false;
    }

    public void SetVisibility(bool visible)
    {
        if (visible)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    private void Update()
    {
        blend += Time.deltaTime * direction;
        blend = Mathf.Clamp01(blend);

        material.SetFloat(propertyName, Mathf.Lerp(minCutoffHeight, maxCutoffHeight, blend));
    }
}
