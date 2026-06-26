using UnityEngine;

public class GameonditionChecker : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private EnemyCounter enemyCounter;
    [SerializeField] private HealthSystem playerHealth;

    private void Start()
    {
        gameTimer.onTimeOut += OnTimeOut;
        enemyCounter.onAllEnemiesDead += OnAllEnemiesDead;
        playerHealth.onDie += OnPlayerDied;

        gameTimer.StartTimer();
    }

    private void OnDestroy()
    {
        gameTimer.onTimeOut -= OnTimeOut;
        enemyCounter.onAllEnemiesDead -= OnAllEnemiesDead;
        playerHealth.onDie -= OnPlayerDied;
    }

    private void OnTimeOut()
    {
        gameManager.GameOver();
    }

    private void OnPlayerDied()
    {
        gameManager.GameOver();
    }

    private void OnAllEnemiesDead()
    {
        gameManager.Victory();
    }
}
