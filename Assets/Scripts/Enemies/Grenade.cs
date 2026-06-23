using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour, IPoolable
{
    [Header("Explosion:")]
    [SerializeField] private int damage = 40;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float fuseTime = 3f;
    [SerializeField] private AudioSource explosionAudio;
    [SerializeField] private LayerMask damageLayer;

    public bool IsActive => gameObject.activeSelf;

    public void Activate()
    {
        gameObject.SetActive(true);
        StartCoroutine(FuseCoroutine());
    }

    public void Deactivate()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    private IEnumerator FuseCoroutine()
    {
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }

    private void Explode()
    {
        //Danio en area
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            explosionRadius,
            damageLayer
            );

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out IDamageable target))
                target.TakeDamage(damage);
        }

        PlayExplosionEffect();
        if (explosionAudio) explosionAudio.Play();

        Deactivate();
    }

    private void PlayExplosionEffect()
    {
        ExplosionParticle explosion = MyPoolManager.Instance.GetInstanceFromPool<ExplosionParticle>();
        if (explosion == null) return;

        explosion.transform.position = transform.position;
        explosion.Activate();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
