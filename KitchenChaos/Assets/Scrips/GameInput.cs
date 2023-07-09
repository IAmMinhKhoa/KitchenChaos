using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDING = "InputBindings";

    public enum Binding
    {
        Move_Up,
        Move_Down, Move_Left, Move_Right,
        Interact,InteractAlternate,
        Pause,
    }

    public static GameInput Instance { get; private set; }   


    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;

    protected PlayerInputSystem playerInputSystems;
    

    private void Awake()
    {
        Instance=this;

        playerInputSystems=new PlayerInputSystem();
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDING))
        {
            playerInputSystems.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDING));
        }
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

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerInputSystems.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputSystems.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputSystems.Player.Move.bindings[4].ToDisplayString();
            case Binding.Move_Right:
                return playerInputSystems.Player.Move.bindings[3].ToDisplayString();
            case Binding.Interact:
                return playerInputSystems.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerInputSystems.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputSystems.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void RebindBingding(Binding binding,Action OnActionRebound)
    {
        playerInputSystems.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputSystems.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputSystems.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputSystems.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Move_Right:
                inputAction = playerInputSystems.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Interact:
                inputAction = playerInputSystems.Player.Move;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputSystems.Player.Move;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputSystems.Player.Move;
                bindingIndex = 0;
                break;
        }


        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            Debug.Log(callback.action.bindings[1].path);
            Debug.Log(callback.action.bindings[1].overridePath);
            callback.Dispose();
            playerInputSystems.Player.Enable();

            OnActionRebound();

            PlayerPrefs.SetString(PLAYER_PREFS_BINDING, playerInputSystems.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

            OnBindingRebind?.Invoke(this, EventArgs.Empty);
        }).Start();
    }

}
