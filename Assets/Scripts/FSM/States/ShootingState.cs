using UnityEngine;

public class ShootingState : StateBase
{
    private float _shootDuration = 0.8f;
    private float _elapsed;

    public ShootingState() => enemyStateType = EnemyStateType.Shooting;

    public override void OnEnter()
    {
        Animator.SetInteger(StateHash, (int)EnemyStateType.Shooting);
        _elapsed = 0f;

        if (Enemy.AttackType == EnemyAttackType.Bullet)
            ShootBullet();
        else
            ShootGrenade();
    }

    public override void OnUpdate()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed >= _shootDuration)
            FSM.ChangeState(EnemyStateType.Aiming);
    }

    private void ShootBullet()
    {
        //Debug.Log($"AttackType: {Enemy.AttackType}");
        //Debug.Log($"MyPoolManager null? {MyPoolManager.Instance == null}");

        EnemyBullet bullet = MyPoolManager.Instance.GetInstanceFromPool<EnemyBullet>();

        //Debug.Log($"Bullet obtenida del pool: {bullet == null}");
        Debug.Log($"Balas activas en pool: {bullet == null}");
        if (bullet == null) return;

        Debug.Log($"FirePoint posicion: {Enemy.FirePoint.position}");
        Debug.Log($"FirePoint forward: {Enemy.FirePoint.forward}");

        bullet.transform.position = Enemy.FirePoint.position;
        bullet.transform.rotation = Enemy.FirePoint.rotation;
        bullet.Activate();

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Debug.Log($"Rigidbody null? {rb == null}");
        rb.linearVelocity = Enemy.FirePoint.forward * 20f;
    }

    private void ShootGrenade()
    {
        Grenade grenade = MyPoolManager.Instance.GetInstanceFromPool<Grenade>();
        if (grenade == null) return;

        grenade.transform.position = Enemy.FirePoint.position;
        grenade.transform.rotation = Enemy.FirePoint.rotation;
        grenade.Activate();

        Vector3 targetPosition = Enemy.Detector.PlayerTransform.position;
        Vector3 direction = (targetPosition - Enemy.FirePoint.position).normalized;
        Vector3 arcDirection = direction + Vector3.up * 0.5f;

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(arcDirection.normalized * Enemy.LaunchForce, ForceMode.Impulse);
    }
}
