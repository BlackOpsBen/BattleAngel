using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    [SerializeField] private float minJumpHeight = 4.0f;

    public void CheckJump(float jumpApexHeight, float jumpLandHeight)
    {
        float jumpHeight = jumpApexHeight - jumpLandHeight;
        if (jumpHeight > minJumpHeight)
        {
            PerformGroundPound();
        }
    }

    private void PerformGroundPound()
    {
        
    }
}
