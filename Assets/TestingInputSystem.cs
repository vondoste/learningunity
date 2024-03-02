using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour{
    private Rigidbody sphereRigidBody;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private void Awake() {
        sphereRigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.LongJump.performed += Long_Jump; // Listens for the long-press and calls the same method.

        // playerInputActions.Player.Disable();
        // playerInputActions.Player.Jump.PerformInteractiveRebinding()
        //    .OnComplete(callback => {
        //        Debug.Log(callback);
        //        callback.Dispose();
        //        playerInputActions.Player.Enable() ;
        //    })
        //    .Start();
        }

    private void Update() {
        if (Keyboard.current.tKey.wasPressedThisFrame) {
            playerInput.SwitchCurrentActionMap("UI");
            playerInputActions.Player.Disable();
            playerInputActions.UI.Enable();
        }
        if (Keyboard.current.yKey.wasPressedThisFrame) {
            playerInput.SwitchCurrentActionMap("Player");
            playerInputActions.UI.Disable();
            playerInputActions.Player.Enable();
        }
    }

    private void FixedUpdate() {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        float speed = 5f;
        sphereRigidBody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }
       
    public void Jump(InputAction.CallbackContext context) {
        Debug.Log(context);
        if (context.performed) {
            Debug.Log("Jump!" + context.phase);
            sphereRigidBody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }

    public void Long_Jump(InputAction.CallbackContext context) {
        Debug.Log(context);
        if (context.performed) {
            Debug.Log("Jump!" + context.phase);
            sphereRigidBody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        }
    }

    public void Submit(InputAction.CallbackContext context) {
        Debug.Log("Submit " + context);
    }
}
