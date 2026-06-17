using System;
//using UnityEngine;

public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
{
    public event Action<int> onScoreChanged;

    private int _currentScore;
    public int CurrentScore => _currentScore;

    public void AddScore(int amount)
    {
        _currentScore += amount;
        onScoreChanged?.Invoke(_currentScore);
    }
}
