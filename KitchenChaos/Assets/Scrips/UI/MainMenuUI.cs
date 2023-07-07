using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] protected Button playButton;
    [SerializeField] protected Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            //click
            Loader.Load(Loader.Scene.GameScene);
        });

        quitButton.onClick.AddListener(() =>
        {
            //click
            Application.Quit();
        });
    }


}
