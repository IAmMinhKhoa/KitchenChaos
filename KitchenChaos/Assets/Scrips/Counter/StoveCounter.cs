using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgessChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }


    [SerializeField] protected FryingRecipeSO[] fryingRecipeSOArr;
    [SerializeField] protected BurningRecipeSO[] burningRecipeSOArr;

    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSo;
    private BurningRecipeSO burningRecipeSo;


    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                    {
                        progressNormalized = (float)fryingTimer / fryingRecipeSo.FryingTimerMax
                    });
                    if (fryingTimer > fryingRecipeSo.FryingTimerMax)
                    {
                        //FRIED
                        GetKitchenObject().DestroySelf();                     
                        KitchenObject.SpawnKitchenObject(fryingRecipeSo.output, this);
                        state = State.Fried ;
                        burningTimer = 0f;
                        
                        burningRecipeSo=GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs
                        {
                            state=state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                    {
                        progressNormalized = (float)burningTimer / burningRecipeSo.BurningTimerMax
                    });
                    if (burningTimer > burningRecipeSo.BurningTimerMax)
                    {
                        //BURNED
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSo.output, this);
                        state = State.Burned;

                        burningRecipeSo = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carrying some thing can be FRIED
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSo = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0f;
                    burningRecipeSo = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                    {
                        progressNormalized = (float)fryingTimer / fryingRecipeSo.FryingTimerMax
                    });
                }
            }
            else
            {
                //player not carrying anything
            }
        }
        else
        {
            //there is a kitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }

            }
            else
            {
                //player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgessChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }


    protected bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);
        return fryingRecipeSO != null;
    }

    protected KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    protected FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArr)
        {
            if (fryingRecipeSO.input == kitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    protected BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArr)
        {
            if (burningRecipeSO.input == kitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
