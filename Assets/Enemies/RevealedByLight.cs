using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealedByLight : MonoBehaviour
{
    [SerializeField] private List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();
    [SerializeField] private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    [SerializeField] private List<MonoBehaviour> toggleWhenRevealeds = new List<MonoBehaviour>();
    [SerializeField] private List<Collider> collidersToToggle = new List<Collider>();
    [SerializeField] private List<Transform> transformsToReLayer = new List<Transform>();
    [SerializeField] private float toggleThreshold = 0.3f;
    private int defaultLayer;

    private string propertyName = "_CutoffHeight";

    private List<Material> skinMeshMaterials = new List<Material>();
    private List<Material> meshMaterials = new List<Material>();

    [SerializeField] private float minCutoffHeight = -0.45f;
    [SerializeField] private float maxCutoffHeight = 5.0f;

    private float blend = 0.0f;

    [SerializeField] private float blendSpeed = 2.0f;

    private int direction = -1;

    private TagToggleManager tagToggle;

    private void Awake()
    {
        tagToggle = GetComponent<TagToggleManager>();
    }

    private void Start()
    {
        for (int i = 0; i < skinnedMeshRenderers.Count; i++)
        {
            Material mat = skinnedMeshRenderers[i].material;
            mat.SetFloat(propertyName, minCutoffHeight);
            skinMeshMaterials.Add(mat);
        }

        for (int i = 0; i < meshRenderers.Count; i++)
        {
            Material mat = meshRenderers[i].material;
            mat.SetFloat(propertyName, minCutoffHeight);
            meshMaterials.Add(mat);
        }

        defaultLayer = gameObject.layer;

        SetToggles(false);
    }

    public void Reveal()
    {
        direction = 1;
    }

    /*public void SetVisibility(bool visible)
    {
        if (visible)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }*/

    private void FixedUpdate()
    {
        UpdateBlendFixed();
        direction = -1;
    }

    private void Update()
    {
        //UpdateBlend();
        UpdateMaterials();
        ToggleItems();
    }

    private void UpdateMaterials()
    {
        for (int i = 0; i < skinMeshMaterials.Count; i++)
        {
            skinMeshMaterials[i].SetFloat(propertyName, Mathf.Lerp(minCutoffHeight, maxCutoffHeight, blend));
        }

        for (int i = 0; i < meshMaterials.Count; i++)
        {
            meshMaterials[i].SetFloat(propertyName, Mathf.Lerp(minCutoffHeight, maxCutoffHeight, blend));
        }
    }

    private void ToggleItems()
    {
        if (blend < toggleThreshold)
        {
            SetToggles(false);
            foreach (Transform mTransform in transformsToReLayer)
            {
                mTransform.gameObject.layer = 2;
            }
        }
        else
        {
            SetToggles(true);
            foreach (Transform mTransform in transformsToReLayer)
            {
                mTransform.gameObject.layer = defaultLayer;
            }
        }
    }

    private void UpdateBlend()
    {
        blend += Time.deltaTime * direction * blendSpeed;
        blend = Mathf.Clamp01(blend);
    }

    private void UpdateBlendFixed()
    {
        blend += Time.fixedDeltaTime * direction * blendSpeed;
        blend = Mathf.Clamp01(blend);
    }

    private void SetToggles(bool active)
    {
        tagToggle.SetIsLit(active);

        foreach (MonoBehaviour item in toggleWhenRevealeds)
        {
            IToggleWhenRevealed toggleable = (IToggleWhenRevealed)item;
            toggleable.ToggleActive(active);
        }

        foreach (Collider mCollider in collidersToToggle)
        {
            mCollider.isTrigger = !active;
        }
    }
}
