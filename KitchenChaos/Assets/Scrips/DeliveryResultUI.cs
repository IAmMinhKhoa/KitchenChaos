using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField] private Image backGroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    [SerializeField] private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed;
        gameObject.SetActive(false);
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e)
    {
        animator.SetTrigger(POPUP); 
        gameObject.SetActive(true);
        backGroundImage.color =  Color.red;
        iconImage.sprite = failedSprite;
        messageText.text = "DELIVERY\nFAILED";
        
       
    }

    private void Instance_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        backGroundImage.color = Color.green;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
        animator.SetTrigger(POPUP);
        
    }
}
