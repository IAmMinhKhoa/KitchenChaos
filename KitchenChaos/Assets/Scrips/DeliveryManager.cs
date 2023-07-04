using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnReciperTimer;
    private float spawnReciperTimerMax=4f;
    private int waitingRecipeMax = 4;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnReciperTimer -= Time.deltaTime;
        if (spawnReciperTimer < 0f)
        {
            spawnReciperTimer = spawnReciperTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }


    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // has the same number of ingredients
                bool plateContentsMathchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjetSO  in waitingRecipeSO.kitchenObjectSOList)
                {
                    //cycling through all ingredients in the recipe
                    bool ingerdientFound = false;
                    foreach  (KitchenObjectSO plateKitchenObjetSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycling through all ingredient in the plate
                        if(plateKitchenObjetSO == recipeKitchenObjetSO)
                        {
                            ingerdientFound = true;
                            break;
                        }
                    }
                    if (!ingerdientFound)
                    {
                        //this recipe ingredient was not found on the plate
                        plateContentsMathchesRecipe = false;    
                    }
                }
                if (plateContentsMathchesRecipe)
                {
                    //player delivered the corret
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        //no matches found

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
}
