 using UnityEngine;

 public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifeTime = 3f;

    //[Header("Feedback")]
    //[SerializeField] private AudioSource impactAudio;

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
        if(collision.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
            target.TakeDamage(damage);

        PlayImpactEffect();
        Deactivate();
    }

    private void PlayImpactEffect()
    {
        //if (impactAudio) impactAudio.Play();

            ImpactParticle impact = MyPoolManager.Instance.GetInstanceFromPool<ImpactParticle>();
            if (impact == null) return;

            impact.transform.position = transform.position;
            impact.transform.rotation = transform.rotation;
            impact.Activate();
    }
}
