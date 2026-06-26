using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayClicked);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
    }

    private void OnPlayClicked()
    {
        LoadingBar.Instance.StartLoadingBar(GoToGameplay);
    }

    private void GoToGameplay()
    {
        CustomSceneManager.Instance.GoToGameplay();
    }
}
