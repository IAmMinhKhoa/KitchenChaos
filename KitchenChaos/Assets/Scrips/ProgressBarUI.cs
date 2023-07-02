using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] protected GameObject hasProgressGameObject;
    [SerializeField] protected Image barImage;
    protected IHasProgress hasProgress;

    private void Start()
    {
        hasProgress=hasProgressGameObject.GetComponent<IHasProgress>(); 
        if(hasProgress == null )
        {
            Debug.LogError("Game Object" + hasProgressGameObject + "does not have conponent needs");
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount= 0;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgessChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if(e.progressNormalized == 0 ||e.progressNormalized==1) {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
