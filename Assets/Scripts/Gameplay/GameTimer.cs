using System;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float totalTime = 120f;

    public event Action onTimeOut;
    public event Action<float> onTimeUpdated;

    private float _remainingTime;
    private bool _isRunning;

    public float RemainingTime => _remainingTime;

    private void Start()
    {
        _remainingTime = totalTime;
    }

    public void StartTimer()
    {
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    private void Update()
    {
        if (!_isRunning) return;

        _remainingTime -= Time.deltaTime;
        onTimeUpdated?.Invoke(_remainingTime);

        if (_remainingTime <= 0f)
        {
            _remainingTime = 0f;
            _isRunning = false;
            onTimeUpdated.Invoke(_remainingTime);
            onTimeOut?.Invoke();
        }
    }
}
