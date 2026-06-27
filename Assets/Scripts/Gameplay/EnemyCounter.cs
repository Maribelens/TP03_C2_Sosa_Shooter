using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public event Action onAllEnemiesDead;
    public event Action<int> onEnemyCountUpdated;

    [SerializeField] private List<EnemyBase> enemies;

    private int _enemiesAlive;

    public int EnemiesAlive => _enemiesAlive;

    private void Start()
    {
        foreach (EnemyBase enemy in enemies)
            RegisterEnemy(enemy);
    }

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

    private void OnDestroy()
    {
        foreach (EnemyBase enemy in enemies)
        {
            if (enemy != null)
                enemy.onDeath -= OnEnemyDied;
        }
    }
}
