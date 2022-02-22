using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealedByLight : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private List<MonoBehaviour> toggleWhenRevealeds = new List<MonoBehaviour>();

    private string tagTarget = "Target";
    private string tagDefault = "Enemy";

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

        if (blend < 0.1f)
        {
            SetToggles(false);
        }
        else
        {
            SetToggles(true);
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
