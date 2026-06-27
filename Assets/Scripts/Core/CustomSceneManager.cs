using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviourSingleton<CustomSceneManager>
{
    public event Action onSceneLoaded;

    [Header("Scenes:")]
    [SerializeField] private string sceneToLoadGameplay;
    [SerializeField] private string sceneToLoadMainMenu;

#if UNITY_EDITOR
    [Header("Inspector: ")]
    [SerializeField] private UnityEditor.SceneAsset _sceneToLoadMainMenu;
    [SerializeField] private UnityEditor.SceneAsset _sceneToLoadGameplay;

    private void OnValidate()
    {
        sceneToLoadMainMenu = _sceneToLoadMainMenu != null ? _sceneToLoadMainMenu.name : "";
        sceneToLoadGameplay = _sceneToLoadGameplay != null ? _sceneToLoadGameplay.name : "";
    }
#endif

    private AsyncOperation _currentAsyncLoad;

    public void GoToMainMenu()
    {
        LoadSceneAsync(sceneToLoadMainMenu);
    }

    public void GoToGameplay()
    {
        LoadSceneAsync(sceneToLoadGameplay);
    }

    public void GoToGameplayImmediate()
    {
        Time.timeScale = 1f;
        LoadSceneImmediate(sceneToLoadGameplay);
    }

    public void GoToMainMenuImmediate()
    {
        Time.timeScale = 1f;
        LoadSceneImmediate(sceneToLoadMainMenu);
    }

    private void LoadSceneImmediate(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = true; // activa inmediatamente
    }

    private void LoadSceneAsync(string sceneName)
    {
        _currentAsyncLoad = SceneManager.LoadSceneAsync(sceneName);
        _currentAsyncLoad.allowSceneActivation = false;
        _currentAsyncLoad.completed += OnSceneLoadedInternal;
    }

    public bool IsSceneReady()
    {
        return _currentAsyncLoad != null && _currentAsyncLoad.progress >= 0.9f;
    }

    public void ActivateLoadedScene()
    {
        if (_currentAsyncLoad != null)
            _currentAsyncLoad.allowSceneActivation = true;
    }

    private void OnSceneLoadedInternal(AsyncOperation operation)
    {
        operation.completed -= OnSceneLoadedInternal;
        onSceneLoaded?.Invoke();
    }
}