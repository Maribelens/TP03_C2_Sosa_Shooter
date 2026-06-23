using UnityEngine;
using System.Collections;

public class EnemyGrenadeLauncher : MonoBehaviour
{
    [Header("Grenade:")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float fireRate = 3f;

    private EnemyDetector _detector;
    private bool _isShooting;

    private void Awake()
    {
        _detector = GetComponent<EnemyDetector>();
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
        _isShooting = false;
    }

    private IEnumerator ShootingRoutine()
    {
        _isShooting = true;

        while (_detector.PlayerInRange)
        {
            LaunchGrenade();
            yield return new WaitForSeconds(fireRate);
        }

        _isShooting = false;
    }

    private void LaunchGrenade()
    {
        Grenade grenade = MyPoolManager.Instance.GetInstanceFromPool<Grenade>();
        if (grenade == null) return;

        grenade.transform.position = firePoint.position;
        grenade.transform.rotation = firePoint.rotation;
        grenade.Activate();

        // Dirección hacia el jugador con arco
        Vector3 targetPosition = _detector.PlayerTransform.position;
        Vector3 direction = (targetPosition - firePoint.position).normalized;
        Vector3 arcDirection = direction + Vector3.up * 0.5f;

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(arcDirection.normalized * launchForce, ForceMode.Impulse);
    }
}
