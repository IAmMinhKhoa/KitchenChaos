using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaceHere;
    public static void ResetStaticData()
    {
        OnAnyObjectPlaceHere = null;
    }


    [SerializeField] protected Transform counterTopPoint;


    protected KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        Debug.Log("interact");
    }
    public virtual void InteractAlternate(Player player)
    {
        Debug.Log("interactAlternate");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null ) {
            OnAnyObjectPlaceHere?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    { return kitchenObject; }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
