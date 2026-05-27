using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiMainMenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
    }

    private void OnPlayButtonClicked()
    {
        if(LoadingBar.Instance)
            LoadingBar.Instance.StartLoadingBar(GoToGameplay);
        else
            Debug.Log("");
    }

    private void GoToGameplay()
    {
        if (CustomSceneManager.Instance)
            CustomSceneManager.Instance.GoToGameplay();
        else
            Debug.Log("");
    }
}
