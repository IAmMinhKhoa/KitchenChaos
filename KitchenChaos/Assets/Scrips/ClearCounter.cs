using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] protected KitchenObjectSO kitchenObjectSO;
    [SerializeField] protected Transform counterTopPoint;
    public void Interact()
    {
        Debug.Log("interact");
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
    }
}
