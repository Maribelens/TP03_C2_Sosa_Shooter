using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiButtonFx : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button:")]
    [SerializeField] private Button button;

    [Header("Animation:")]
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private float animDuration = 0.2f;
    [SerializeField] private AnimationCurve animationCurve;

    private IEnumerator _expanding;

    private void Awake()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked() { }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RunCoroutine(Expanding());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RunCoroutine(Collapsing());
    }

    private void RunCoroutine(IEnumerator coroutine)
    {
        if (_expanding != null)
            StopCoroutine(_expanding);
        _expanding = coroutine;
        StartCoroutine(_expanding);
    }

    private IEnumerator Expanding()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = Vector3.one * hoverScale;
        float onTime = 0f;
        float remaining = (targetScale.x - initialScale.x) / (targetScale.x - Vector3.one.x);
        float duration = remaining * animDuration;

        while (onTime < duration)
        {
            onTime += Time.unscaledDeltaTime;
            float lerp = onTime / duration;
            transform.localScale = initialScale + (targetScale - initialScale) * animationCurve.Evaluate(lerp);
            yield return null;
        }

        transform.localScale = targetScale;
        _expanding = null;
    }

    private IEnumerator Collapsing()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = Vector3.one;
        float onTime = 0f;
        float remaining = initialScale.x - targetScale.x;
        float duration = remaining * animDuration;

        while (onTime < duration)
        {
            onTime += Time.unscaledDeltaTime;
            float lerp = onTime / duration;
            transform.localScale = initialScale - (initialScale - targetScale) * lerp;
            yield return null;
        }

        transform.localScale = targetScale;
        _expanding = null;
    }
}
