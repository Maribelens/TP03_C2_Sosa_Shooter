using System;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class EnemyBase : MonoBehaviour
{
    [Header("Score:")]
    [SerializeField] private int scoreValue = 50;

    [Header("Feedback:")]
    [SerializeField] private AudioSource deathAudio;

    public event Action onDeath;

    private HealthSystem _healthSystem;
    private bool _isDead;

    protected virtual void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.onDie += OnDie;
        _healthSystem.onLifeUpdated += OnLifeUpdated;
    }

    private void OnDestroy()
    {
        _healthSystem.onDie -= OnDie;
        _healthSystem.onLifeUpdated -= OnLifeUpdated;
    }

    private void OnLifeUpdated(float current, float max)
    {
        if (current < max)
            GetComponent<FSMManager>()?.ChangeState(EnemyStateType.Hurt);
    }

    private void OnDie()
    {
        if (_isDead) return;
        _isDead = true;

        GetComponent<FSMManager>()?.ChangeState(EnemyStateType.Death);
        PlayDeathEffect();
        if (deathAudio) deathAudio.Play();

        onDeath?.Invoke();
        ScoreManager.Instance.AddScore(scoreValue);
        Destroy(gameObject, 2f);
    }

    private void PlayDeathEffect()
    {
        ExplosionParticle death = MyPoolManager.Instance.GetInstanceFromPool<ExplosionParticle>();
        if (death == null) return;

        death.transform.position = transform.position;
        death.Activate();
    }
}
