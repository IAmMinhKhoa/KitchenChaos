using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] protected Button resumeButton;
    [SerializeField] protected Button mainMenuButton;


    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += Instance_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += Instance_OnGameUnpaused;
        Hide();
    }

    private void Instance_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Instance_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    protected void Show()
    {
        gameObject.SetActive(true);
    }
    protected void Hide()
    {
        gameObject.SetActive(false);
    }
}
