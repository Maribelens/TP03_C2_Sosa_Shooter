using UnityEngine;
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
    }

    private void GoToGameplay()
    {
        if (CustomSceneManager.Instance)
            CustomSceneManager.Instance.GoToGameplay();
    }
}
