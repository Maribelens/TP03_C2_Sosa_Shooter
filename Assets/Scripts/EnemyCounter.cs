using System;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public event Action onAllEnemiesDead;
    public event Action<int> onEnemyCountUpdated;

    private int _enemiesAlive;

    public int EnemiesAlive => _enemiesAlive;

    public void RegisterEnemy(EnemyBase enemy)
    {
        _enemiesAlive++;
        enemy.onDeath += OnEnemyDied;
        onEnemyCountUpdated?.Invoke(_enemiesAlive);
    }

    private void OnEnemyDied()
    {
        _enemiesAlive--;
        onEnemyCountUpdated?.Invoke(_enemiesAlive);

        if (_enemiesAlive <= 0)
            onAllEnemiesDead?.Invoke();
    }
}
