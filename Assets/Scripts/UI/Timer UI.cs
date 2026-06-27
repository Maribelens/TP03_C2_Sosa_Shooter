using TMPro;
using UnityEngine;
public class UiTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameTimer gameTimer;

    private void OnEnable()
    {
        if (gameTimer == null) return;
        gameTimer.onTimeUpdated += UpdateTimerText;
    }

    private void OnDisable()
    {
        if (gameTimer == null) return;
        gameTimer.onTimeUpdated -= UpdateTimerText;
    }

    private void UpdateTimerText(float remainingTime)
    {
        //remainingTime = Mathf.Max(0f, remainingTime);
        
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
