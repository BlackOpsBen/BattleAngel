using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagToggleManager : MonoBehaviour
{
    private bool isLit = false;
    private bool isWounded = false;

    [SerializeField] private List<TargetTransform> transformsToTag = new List<TargetTransform>();

    [SerializeField] private string targetTag = "Target";
    [SerializeField] private string defaultTag = "Enemy";

    private void Update()
    {
        ToggleTags();
    }

    private void ToggleTags()
    {
        if (isLit)
        {
            foreach (TargetTransform target in transformsToTag)
            {
                if (isWounded && target.disableIfWounded)
                {
                    target.transform.tag = defaultTag;
                }
                else
                {
                    target.transform.tag = targetTag;
                }
            }
        }
        else
        {
            foreach (TargetTransform target in transformsToTag)
            {
                target.transform.tag = defaultTag;
            }
        }
    }

    public void SetIsLit(bool value)
    {
        isLit = value;
    }

    public void SetIsWounded(bool value)
    {
        isWounded = value;
    }

    [System.Serializable]
    private struct TargetTransform
    {
        public Transform transform;
        public bool disableIfWounded;
    }
}
