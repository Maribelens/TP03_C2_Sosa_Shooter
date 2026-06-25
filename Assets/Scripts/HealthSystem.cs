using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    public event Action<float, float> onLifeUpdated;
    public event Action onDie;

    [Header("Health:")]
    [SerializeField] private float maxHealth = 100f;

    private float _currentHealth;
    private bool _isDead;

    public bool IsInvulnerable { get; set; }
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => maxHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    private void Start()
    {
        onLifeUpdated?.Invoke(_currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (amount < 0 || IsInvulnerable || _isDead) return;

        _currentHealth = Mathf.Max(0, _currentHealth - amount);
        onLifeUpdated?.Invoke(_currentHealth, maxHealth);

        if (_currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        if (amount < 0 || _isDead) return;

        _currentHealth = Mathf.Min(maxHealth, _currentHealth + amount);
        onLifeUpdated?.Invoke(_currentHealth, maxHealth);
    }

    private void Die()
    {
        if (_isDead) return;
        _isDead = true;
        onDie?.Invoke();
    }
}
