using UnityEngine;

public interface IPoolable
{
    bool IsActive { get; }
    void Activate();
    void Deactivate();
}

//[Header("Laser")]
//[SerializeField] private LineRenderer laserLine;
//[SerializeField] private float aimDuration = 2f;
//[SerializeField] private Transform firePoint;

//[Header("Shoot")]
//[SerializeField] private float bulletSpeed = 15f;

//private StaticEnemy _detection;
//private float _aimTimer;
//private bool _isAiming;

//private void Awake() => _detection = GetComponent<StaticEnemy>();

//private void Update()
//{
//    if (!_detection.IsPlayerInRange)
//    {
//        StopAiming();
//        return;
//    }
//    HandleAimCycle();
//}

//private void HandleAimCycle()
//{
//    if (!_isAiming) StartAiming();

//    _aimTimer += Time.deltaTime;
//    UpdateLaserVisual();

//    if (_aimTimer >= aimDuration)
//    {
//        Shoot();
//        _aimTimer = 0f;
//    }
//}

//private void StartAiming()
//{
//    _isAiming = true;
//    laserLine.enabled = true;
//}

//private void StopAiming()
//{
//    _isAiming = false;
//    _aimTimer = 0f;
//    laserLine.enabled = false;
//}

//private void UpdateLaserVisual()
//{
//    laserLine.SetPosition(0, firePoint.position);
//    laserLine.SetPosition(1, _detection.PlayerTarget.position);
//}

//private void Shoot()
//{
//    EnemyBullet bullet = MyPoolManager.Instance.GetInstanceFromPool<EnemyBullet>();
//    if (bullet == null) return;
//    bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
//    Vector3 dir = (_detection.PlayerTarget.position - firePoint.position).normalized;
//    bullet.Activate(dir, bulletSpeed);
//}