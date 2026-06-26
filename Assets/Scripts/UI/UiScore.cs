using TMPro;
using UnityEngine;
public class UiScore : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        ScoreManager.Instance.onScoreChanged += UpdateScoreText;
        UpdateScoreText(ScoreManager.Instance.CurrentScore);
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.onScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int newScore)
    {
        scoreText.text = $"{newScore}";
    }
}
