 using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    private void Start()
    {
        LoadingBar.Instance.StartLoadingBar(GoToMainMenu);
    }

    private void GoToMainMenu()
    {
        CustomSceneManager.Instance.GoToMainMenu();
    }
}
