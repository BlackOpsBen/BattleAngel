using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealedByLight : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private List<MonoBehaviour> toggleWhenRevealeds = new List<MonoBehaviour>();
    [SerializeField] private Collider colliderToToggle;
    [SerializeField] private float toggleThreshold = 0.3f;

    private string tagTarget = "Target";
    private string tagDefault = "Enemy";
    private int defaultLayer;

    private string propertyName = "_CutoffHeight";

    private Material material;

    private float minCutoffHeight = -0.45f;
    private float maxCutoffHeight = 5.0f;

    private float blend = 0.0f;

    [SerializeField] private float blendSpeed = 2.0f;

    private int direction = -1;

    private void Start()
    {
        material = skinnedMeshRenderer.material;
        material.SetFloat(propertyName, minCutoffHeight);

        defaultLayer = gameObject.layer;

        SetToggles(false);
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
        UpdateBlend();

        material.SetFloat(propertyName, Mathf.Lerp(minCutoffHeight, maxCutoffHeight, blend));

        if (blend < toggleThreshold)
        {
            SetToggles(false);
            gameObject.layer = 2;
        }
        else
        {
            SetToggles(true);
            gameObject.layer = defaultLayer;
        }
    }

    private void UpdateBlend()
    {
        blend += Time.deltaTime * direction * blendSpeed;
        blend = Mathf.Clamp01(blend);
    }

    private void SetToggles(bool active)
    {
        SetTag(active);

        foreach (MonoBehaviour item in toggleWhenRevealeds)
        {
            IToggleWhenRevealed toggleable = (IToggleWhenRevealed)item;
            toggleable.ToggleActive(active);
        }

        colliderToToggle.isTrigger = !active;
    }

    private void SetTag(bool active)
    {
        if (active)
        {
            gameObject.tag = tagTarget;
        }
        else
        {
            gameObject.tag = tagDefault;
        }
    }
}
