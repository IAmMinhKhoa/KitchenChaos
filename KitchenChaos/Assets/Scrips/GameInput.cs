using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }   


    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    
    protected PlayerInputSystem playerInputSystems;
    

    private void Awake()
    {
        Instance=this;

        playerInputSystems=new PlayerInputSystem();
        playerInputSystems.Player.Enable();

        playerInputSystems.Player.Interact.performed += Interact_performed;
        playerInputSystems.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputSystems.Player.Pause.performed += Pause_performed;
    }
    private void OnDestroy()
    {
        playerInputSystems.Player.Interact.performed -= Interact_performed;
        playerInputSystems.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputSystems.Player.Pause.performed -= Pause_performed;

        playerInputSystems.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
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
