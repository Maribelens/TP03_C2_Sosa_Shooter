using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadingBar : MonoBehaviourSingleton<LoadingBar>
{
    public event Action<string> onLoadingBarFinished;
    private event Action currentCallback;

    [SerializeField] private Vector2 loadingTime = new Vector2(1, 3);
    [SerializeField] private Image image;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private CanvasGroup canvasGroup;

    protected IEnumerator loadingBarCoroutine;
    private bool isSceneLoadingYet;

    public bool IsFinished => loadingBarCoroutine == null;

    protected override void OnAwaken() { }

    private void Start()
    {
        CustomSceneManager.Instance.onSceneLoadeed += Intance_onSceneLoaded;
        SetState(false);
    }

    protected override void OnDestroyed() 
    {
        //if (CustomSceneManager.Instance != null)
        CustomSceneManager.Instance.onSceneLoadeed -= Intance_onSceneLoaded;
    }

    public void StartLoadingBar(Action callback)
    {
        if (loadingBarCoroutine == null)
        {
            currentCallback = callback;
            loadingBarCoroutine = LoadingBarCoroutine();
            StartCoroutine(LoadingBarCoroutine());
        }
    }

    IEnumerator LoadingBarCoroutine()
    {
        SetState(true);

        float currentTime = 0;
        float maxLoadingTime = Random.Range(loadingTime.x, loadingTime.y);

        currentCallback?.Invoke(); // avisa al SceneManager que empiece a cargar
        isSceneLoadingYet = true;

        while (currentTime < maxLoadingTime)
        {
            if (currentTime > maxLoadingTime / 2 && isSceneLoadingYet)
            {
                yield return null;
                continue;
            }

            currentTime += Time.deltaTime;
            float lerp = curve.Evaluate(currentTime / maxLoadingTime);
            image.fillAmount = lerp;
            yield return null;
        }
        loadingBarCoroutine = null;
        SetState(false);
    }

    private void Intance_onSceneLoaded()
    {
        isSceneLoadingYet = false;
    }

    private void SetState(bool isOn)
    {
        canvasGroup.alpha = isOn ? 1 : 0;
        canvasGroup.interactable = isOn;
        canvasGroup.blocksRaycasts = isOn;
    }
}