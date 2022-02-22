using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamRotateView : MonoBehaviour
{
    private float rawInput = 0.0f;
    public void OnRotateView(InputAction.CallbackContext context)
    {
        rawInput = context.ReadValue<float>();
    }

    private void Update()
    {
        transform.Rotate(0.0f, rawInput, 0.0f);
    }
}
