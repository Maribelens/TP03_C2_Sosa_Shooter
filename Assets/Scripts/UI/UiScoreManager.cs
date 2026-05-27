using TMPro;
using UnityEngine;

public class UiScoreManager : MonoBehaviourSingleton<UiScoreManager>
{
    [SerializeField] private TMP_Text scoreText;
    private int score;

    protected override void OnAwaken()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }
}
