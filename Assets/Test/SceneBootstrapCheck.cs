using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1000)]
public class SceneBootstrapCheck : MonoBehaviour
{
    [SerializeField] private string bootstrapSceneName = "SplashScreen";

    private void Awake()
    {
        if (MyPoolManager.Instance == null)
        {
            SceneManager.LoadScene(bootstrapSceneName, LoadSceneMode.Additive);
        }
    }
}
