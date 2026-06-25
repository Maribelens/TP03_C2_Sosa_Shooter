using UnityEngine;

public class TestTarget : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int scoreValue = 10;

    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        Debug.Log($"{name} recibio {amount} de damage. Vida restante: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            ScoreManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}

