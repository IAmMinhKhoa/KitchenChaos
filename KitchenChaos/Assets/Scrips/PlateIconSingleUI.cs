using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image imgage;
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        imgage.sprite = kitchenObjectSO.sprite;
    }
}
