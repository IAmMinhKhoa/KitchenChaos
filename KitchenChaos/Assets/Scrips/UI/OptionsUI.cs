using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] protected Button soundEffectsButton;
    [SerializeField] protected Button musicButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;


    private void Awake()
    {
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {

        });
    }
    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        soundEffectText.text ="Sound Effects : "+Mathf.Round( SoundManager.Instance.GetVolume()*10f);
    }
}
