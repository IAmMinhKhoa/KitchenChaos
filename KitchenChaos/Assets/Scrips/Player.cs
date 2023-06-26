using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] protected float moveSpeed=7f;
    protected float rotateSpeed=10f;
    protected bool isWalking;
    // Update is called once per frame
    private void Update() {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode. W)){ 
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S)) { 
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A)) { 
            inputVector.x = -1;
        }
        if (Input.GetKey (KeyCode.D)) { 
            inputVector.x = +1;
        }
        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); 
        isWalking=moveDir!= Vector3.zero;
        transform.position+=moveDir*moveSpeed*Time.deltaTime;
        transform.forward=Vector3.Slerp(transform.forward,moveDir,Time.deltaTime*rotateSpeed);
    }
    public bool IsWalking(){
        return isWalking;
    }
}
