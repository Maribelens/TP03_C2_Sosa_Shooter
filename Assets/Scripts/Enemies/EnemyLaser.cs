using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour
{
    [Header("Laser:")]
    [SerializeField] private LineRenderer laserRenderer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float aimDuration = 2f;

    [Header("Shooting:")]
    [SerializeField] private float fireRate = 1.5f;

    private EnemyDetector _detector;
    private bool _isShooting;

    private void Awake()
    {
        _detector = GetComponent<EnemyDetector>();
        laserRenderer.enabled = false;
    }

    private void Start()
    {
        _detector.onPlayerEnterRange += OnPlayerEnterRange;
        _detector.onPlayerExitRange += OnPlayerExitRange;
    }

    private void OnDestroy()
    {
        _detector.onPlayerEnterRange -= OnPlayerEnterRange;
        _detector.onPlayerExitRange -= OnPlayerExitRange;
    }

    private void OnPlayerEnterRange()
    {
        if (!_isShooting)
            StartCoroutine(ShootingRoutine());
    }

    private void OnPlayerExitRange()
    {
        StopAllCoroutines();
        laserRenderer.enabled = false;
        _isShooting = false;
    }

    private IEnumerator ShootingRoutine()
    {
        _isShooting = true;

        while (_detector.PlayerInRange)
        {
            yield return StartCoroutine(AimRoutine()); //Apuntar con laser

            if (_detector.PlayerInRange)
                Shoot();

            yield return new WaitForSeconds(fireRate);
        }

        _isShooting = false;
    }

    private IEnumerator AimRoutine()
    {
        laserRenderer.enabled = true;

        float elapsed = 0f;
        while (elapsed < aimDuration)
        {
            if (_detector.PlayerTransform != null)
            {
                laserRenderer.SetPosition(0, firePoint.position);
                laserRenderer.SetPosition(1, _detector.PlayerTransform.position);

                transform.LookAt(_detector.PlayerTransform);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        laserRenderer.enabled = false;
    }

    private void Shoot()
    {
        EnemyBullet bullet = MyPoolManager.Instance.GetInstanceFromPool<EnemyBullet>();
        if (bullet == null) return;

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.Activate();

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * 20f;
    }
}
