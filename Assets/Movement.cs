using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private CharacterController characterController;

    private Vector2 movementRawInput;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementRawInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector3 move = new Vector3(movementRawInput.x, 0.0f, movementRawInput.y);
        characterController.Move(move * Time.deltaTime * moveSpeed);
    }
}
