using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseMenu pause = GameManager.Instance.GetComponent<PauseMenu>();
            pause.SetPause(!pause.GetIsPaused());
        }
    }
}
