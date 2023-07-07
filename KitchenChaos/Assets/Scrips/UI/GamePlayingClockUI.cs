using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] protected Image imageClock;

    private void Update()
    {
        imageClock.fillAmount = KitchenGameManager.Instance.GetTimePlayingTimerNormalized();
    }
}
