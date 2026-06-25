using UnityEngine;

public class EnemyBullet : MonoBehaviour, IPoolable
{
    [SerializeField] private int damage = 15;
    [SerializeField] private float lifeTime = 4f;

    private float _timer;

    public bool IsActive => gameObject.activeSelf;

    public void Activate()
    {
        gameObject.SetActive(true);
        _timer = 0f;
    }

    public void Deactivate()
    {
        Debug.Log("EnemyBullet desactivada");
        gameObject.SetActive(false);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= lifeTime)
            Deactivate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
            target.TakeDamage(damage);

        Deactivate();
    }
}
