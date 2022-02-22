using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    [SerializeField] private float minJumpHeight = 1.0f;

    public void CheckJump(float jumpApexHeight, float jumpLandHeight)
    {
        if (jumpApexHeight - jumpLandHeight > minJumpHeight)
        {
            PerformGroundPound();
        }
    }

    private void PerformGroundPound()
    {
        Debug.Log("Ground Pound!");
    }
}
