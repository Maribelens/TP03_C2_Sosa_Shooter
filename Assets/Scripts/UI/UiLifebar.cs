using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiLifebar : MonoBehaviour
{
    [Header("LifeBar Panel")]
    [SerializeField] private HealthSystem lifeTarget;
    [SerializeField] private Image lifeBar;
    [SerializeField] private TMP_Text lifeText;

    private void Awake()
    {
        if (lifeTarget == null) return;

        lifeTarget.onLifeUpdated += UpdateLifeBar;
        lifeTarget.onDie += OnDie;
    }

    private void OnDestroy()
    {
        if (lifeTarget == null) return;

        lifeTarget.onLifeUpdated -= UpdateLifeBar;
        lifeTarget.onDie -= OnDie;
    }

    public void UpdateLifeBar(float current, float max)
    {
        if (max <= 0f) return;

        if (lifeBar != null)
            lifeBar.fillAmount = current / max;

        if (lifeText != null)
            lifeText.text = $"{current:0}/{max:0}";
    }

    private void OnDie()
    {
        UpdateLifeBar(0, lifeTarget.MaxHealth);
        gameObject.SetActive(false);
    }
}
