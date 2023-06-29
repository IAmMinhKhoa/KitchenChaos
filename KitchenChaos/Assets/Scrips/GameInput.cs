using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    protected PlayerInputSystem playerInputSystems;
    
    private void Awake()
    {
        playerInputSystems=new PlayerInputSystem();
        playerInputSystems.Player.Enable();

        playerInputSystems.Player.Interact.performed += Interact_performed;
        playerInputSystems.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
       
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputSystems.Player.Move.ReadValue<Vector2>();  ;
        
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
