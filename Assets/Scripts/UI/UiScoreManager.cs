using TMPro;
using UnityEngine;

public class UiScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int score;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AddScore(100);

        if (Input.GetKeyDown(KeyCode.R))
            ResetScore();
    }

    private void Awake()
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
