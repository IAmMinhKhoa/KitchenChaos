using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }


    [SerializeField] private AudioPresSO audioPresSO;

    private void Awake()
    {
        Instance = this;    
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Instance_OnPickedSomething;
        BaseCounter.OnAnyObjectPlaceHere += BaseCounter_OnAnyObjectPlaceHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter =sender as TrashCounter;
        PlaySound(audioPresSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaceHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioPresSO.objectDrop, baseCounter.transform.position);
    }

    private void Instance_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioPresSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioPresSO.chop, cuttingCounter.transform.position);
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter =DeliveryCounter.Instance;
        PlaySound(audioPresSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioPresSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    private void PlaySound(AudioClip audioClip,Vector3 position,float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip,position,volume);
    }

    public void PlayFootstepsSound(Vector3 position,float volume)
    {
        PlaySound(audioPresSO.footstep, position,volume); 
    }
}
