using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Shooting:")] 
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float fireRate = 0.2f;

    [Header("Feedback:")] 
    [SerializeField] private AudioSource shootAudio;

    private float _nextFireTime;

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {

        Bullet bullet = MyPoolManager.Instance.GetInstanceFromPool<Bullet>();
        if(bullet == null) return;

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.Activate();

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * bulletSpeed;

        PlayMuzzleFlashEffect();
        if (shootAudio) shootAudio.Play();
    }

    private void PlayMuzzleFlashEffect()
    {
        MuzzleFlashParticle muzzle = MyPoolManager.Instance.GetInstanceFromPool<MuzzleFlashParticle>();
        if(muzzle == null) return;

        muzzle.transform.position = firePoint.position;
        muzzle.transform.rotation = firePoint.rotation;
        muzzle.Activate();
    }
}
