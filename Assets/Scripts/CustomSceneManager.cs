using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviourSingleton<CustomSceneManager>
{
    public event Action onSceneLoadeed;

    [Header("Scenes:")]
    [SerializeField] private string sceneToLoadGameplay;

#if UNITY_EDITOR
    [Header("Inspector: ")]
    [SerializeField] private UnityEditor.SceneAsset _sceneToLoadGameplay;

    private void OnValidate()
    {
        sceneToLoadGameplay = _sceneToLoadGameplay.name;
    }
#endif

    public void GoToGameplay()
    {
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoadGameplay);
        asyncLoad.allowSceneActivation = false; //clave: carga pero no activa

        // Espera hasta que esté lista (progress llega a 0.9 cuando allowSceneActivation es false)
        while (asyncLoad.progress < 0.9f)
            yield return null;

        // Avisa que terminó de cargar
        onSceneLoadeed?.Invoke();

        // Espera a que la LoadingBar diga que terminó antes de activar
        yield return new WaitUntil(() => LoadingBar.Instance.IsFinished);

        asyncLoad.allowSceneActivation = true; //recién ahora cambia la escena
    }

    private void OnSceneLoadedInternal(AsyncOperation obj)
    {
        obj.completed -= OnSceneLoadedInternal;
        onSceneLoadeed?.Invoke();
    }
}