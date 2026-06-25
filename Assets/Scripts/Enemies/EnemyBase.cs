using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    //[SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private AudioSource deathAudio;

    public event Action onDeath;
    private int _currentHealth;

    [SerializeField] private int scoreValue = 50;

    protected void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        Debug.Log($"{name} recibio {amount} de damage. Vida restante: {_currentHealth}");

        if (_currentHealth <= 0)
            Die();
        else
            GetComponent<FSMManager>()?.ChangeState(EnemyStateType.Hurt);
    }

    private void Die()
    {
        GetComponent<FSMManager>()?.ChangeState(EnemyStateType.Death);
        PlayDeathEffect();
        if (deathAudio) deathAudio.Play();

        onDeath?.Invoke();
        ScoreManager.Instance.AddScore(scoreValue);

        Destroy(gameObject, 2f); // delay para que se vean partículas/animación
    }

    private void PlayDeathEffect()
    {
        ExplosionParticle death = MyPoolManager.Instance.GetInstanceFromPool<ExplosionParticle>();
        if (death == null) return;

        death.transform.position = transform.position;
        death.Activate();
    }
}
