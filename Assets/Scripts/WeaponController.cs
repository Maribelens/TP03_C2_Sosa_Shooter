using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Shooting:")] 
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float fireRate = 0.2f;

    //[Header("Feedback:")] 
    //[SerializeField] private ParticleSystem muzzleFlash;
    //[SerializeField] private AudioSource shootAudio;

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
        Debug.Log($"MyPoolManager.Instance es null? {MyPoolManager.Instance == null}");

        Bullet bullet = MyPoolManager.Instance.GetInstanceFromPool<Bullet>();
        if(bullet == null)
        {
            Debug.Log("GetInstanceFromPool devolvio null");
            return;
        }

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.Activate();

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * bulletSpeed;

        //PlayFeedback();
    }

    //private void PlayFeedback()
    //{
    //    if(muzzleFlash) muzzleFlash.Play();
    //    if(shootAudio) shootAudio.Play();
    //}
}
