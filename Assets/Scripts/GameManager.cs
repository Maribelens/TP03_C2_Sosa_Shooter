using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        Victory
    }

    [SerializeField] private GameState currentState;
    public GameState CurrentGameState => currentState;

    public event Action<GameState> OnStateChanged;
    public event Action OnGameOver;
    public event Action OnVictory;

    private Dictionary<GameState, Action> stateActions;

    private void Awake()
    {
        // Inicializacion de acciones por estado
        stateActions = new Dictionary<GameState, Action>
        {
            { GameState.Playing, () => ApplyTimeScale(1f) },
            { GameState.Paused, () => ApplyTimeScale(0f) },
            {
                GameState.GameOver, () =>
                {
                    ApplyTimeScale(0f);
                    OnGameOver?.Invoke();
                }
            },
            {
                GameState.Victory, () =>
                {
                    ApplyTimeScale(0f);
                    OnVictory?.Invoke();
                }
            }
        };
    }

    private void Start()
    {
        SetGameState(GameState.Playing);
    }

    private void SetGameState(GameState newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        OnStateChanged?.Invoke(currentState);

        if (stateActions.TryGetValue(newState, out var action))
        {
            action.Invoke();
        }
    }

    private void ApplyTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    // Métodos públicos que cambian estado
    public void GameOver() => SetGameState(GameState.GameOver);
    public void Victory() => SetGameState(GameState.Victory);
    public void Pause() => SetGameState(GameState.Paused);
    public void Resume() => SetGameState(GameState.Playing);

    private void OnDestroy()
    {
        // Limpieza de eventos para evitar referencias colgadas
        OnStateChanged = null;
        OnGameOver = null;
        OnVictory = null;
    }
}
