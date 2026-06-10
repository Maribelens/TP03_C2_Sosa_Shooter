using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadingBar : MonoBehaviourSingleton<LoadingBar>
{
    public event Action onLoadingBarFinished;

    private Action _currentCallback;

    [SerializeField] private Vector2 loadingTime = new Vector2(1, 3);
    [SerializeField] private Image image;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private CanvasGroup canvasGroup;

    private IEnumerator _loadingBarCoroutine;

    public bool IsFinished => _loadingBarCoroutine == null;

    protected override void OnAwaken()
    {
        SetState(false);
    }

    public void StartLoadingBar(Action callback)
    {
        if (_loadingBarCoroutine != null) return;

        _currentCallback = callback;
        _loadingBarCoroutine = LoadingBarCoroutine();
        StartCoroutine(_loadingBarCoroutine);
    }

    private IEnumerator LoadingBarCoroutine()
    {
        SetState(true);

        _currentCallback?.Invoke();

        float currentTime = 0;
        float maxLoadingTime = Random.Range(loadingTime.x, loadingTime.y);


        while (currentTime < maxLoadingTime || !CustomSceneManager.Instance.IsSceneReady())
        {
            if (!CustomSceneManager.Instance.IsSceneReady())
            {
                yield return null;
                continue;
            }

            currentTime += Time.deltaTime;
            float lerp = curve.Evaluate(currentTime / maxLoadingTime);
            image.fillAmount = lerp;
            yield return null;
        }

        image.fillAmount = 1f;
        CustomSceneManager.Instance.ActivateLoadedScene();

        SetState(false);
        _loadingBarCoroutine = null;
        onLoadingBarFinished?.Invoke();
    }

    private void SetState(bool isOn)
    {
        canvasGroup.alpha = isOn ? 1 : 0;
        canvasGroup.interactable = isOn;
        canvasGroup.blocksRaycasts = isOn;
    }
}