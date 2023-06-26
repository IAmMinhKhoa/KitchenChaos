using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] protected float moveSpeed=7f;
    [SerializeField] protected GameInput gameInput;
    protected float rotateSpeed=10f;
    protected bool isWalking;
   
    private void Update() {
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance =moveSpeed*Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = 0.7f;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up* playerHeight, playerRadius, moveDir, moveDistance);

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking=moveDir!= Vector3.zero;
        
        transform.forward=Vector3.Slerp(transform.forward,moveDir,Time.deltaTime*rotateSpeed);
    }
    public bool IsWalking(){
        return isWalking;
    }
}
