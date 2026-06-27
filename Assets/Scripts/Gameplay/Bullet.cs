 using UnityEngine;

 public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifeTime = 3f;

    private float _timer;

    public bool IsActive => gameObject.activeSelf;

    public void Activate()
    {
        gameObject.SetActive(true);
        _timer = 0f;
    }

    public void Deactivate()
    {
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
        IDamageable target = collision.gameObject.GetComponentInParent<IDamageable>();

        if (target != null)
            target.TakeDamage(damage);

        PlayImpactEffect();
        Deactivate();
    }

    private void PlayImpactEffect()
    {
        ImpactParticle impact = MyPoolManager.Instance.GetInstanceFromPool<ImpactParticle>();
        if (impact == null) return;

        impact.transform.position = transform.position;
        impact.transform.rotation = transform.rotation;
        impact.Activate();
    }
}
