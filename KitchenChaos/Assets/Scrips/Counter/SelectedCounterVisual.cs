using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] protected BaseCounter baseCounter;
    [SerializeField] protected GameObject[] visualGameobject;
    
    private void Start()
    { 
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged; ;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    protected void Show()
    {
        foreach (GameObject visual in visualGameobject) {
            visual.SetActive(true);

        }
    }
    protected void Hide()
    {
        foreach (GameObject visual in visualGameobject)
        {
            visual.SetActive(false);

        }
    }
}
