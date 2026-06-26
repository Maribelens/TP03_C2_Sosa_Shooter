using UnityEngine;

public class AimingState : StateBase
{
    private float _aimDuration = 2f;
    private float _elapsed;

    public AimingState() => enemyStateType = EnemyStateType.Aiming;

    public override void OnEnter()
    {
        Animator.SetInteger(StateHash, (int)EnemyStateType.Aiming);
        _elapsed = 0f;
    }

    public override void OnUpdate()
    {
        if (!Enemy.Detector.PlayerInRange)
        {
            FSM.ChangeState(EnemyStateType.Idle);
            return;
        }

        // Rotar hacia el jugador
        Enemy.transform.LookAt(Enemy.Detector.PlayerTransform);

        _elapsed += Time.deltaTime;
        if (_elapsed >= _aimDuration)
            FSM.ChangeState(EnemyStateType.Shooting);
    }
}
