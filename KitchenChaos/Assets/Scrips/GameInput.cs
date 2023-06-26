using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    protected PlayerInputSystem playerInputSystems;
    private void Awake()
    {
        playerInputSystems=new PlayerInputSystem();
        playerInputSystems.Player.Enable();
    }
    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputSystems.Player.Move.ReadValue<Vector2>();  ;
        
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
