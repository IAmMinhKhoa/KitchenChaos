using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] protected ClearCounter clearCounter;
    [SerializeField] protected GameObject visualGameobject;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged; ;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        Debug.Log("gr");
        if (e.selectedCounter == clearCounter)
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
        visualGameobject.SetActive(true);
    }
    protected void Hide()
    {
        visualGameobject.SetActive(false);
    }
}
