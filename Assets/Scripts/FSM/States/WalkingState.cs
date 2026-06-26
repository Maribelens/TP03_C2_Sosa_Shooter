using UnityEngine;
using UnityEngine.AI;

public class WalkingState : StateBase
{
    private float _idleCheckTimer;
    private float _idleCheckInterval;
    public WalkingState() => enemyStateType = EnemyStateType.Walking;

    public override void OnEnter()
    {
        Animator.SetInteger(StateHash, (int)EnemyStateType.Walking);
        Enemy.Agent.isStopped = false;
        SetRandomDestination();
        ResetIdleTimer();
    }

    public override void OnUpdate()
    {
        if (Enemy.Detector.PlayerInRange)
        {
            FSM.ChangeState(EnemyStateType.Aiming);
            return;
        }

        if (!Enemy.Agent.pathPending && Enemy.Agent.remainingDistance < 0.5f)
            SetRandomDestination();

        _idleCheckTimer -= Time.deltaTime;
        if (_idleCheckTimer <= 0f)
            FSM.ChangeState(EnemyStateType.Idle);
    }

    public override void OnExit()
    {
        Enemy.Agent.isStopped = true;
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * Enemy.PatrolRadius;
        randomDirection += Enemy.transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, Enemy.PatrolRadius, NavMesh.AllAreas))
            Enemy.Agent.SetDestination(hit.position);
    }

    private void ResetIdleTimer()
    {
        _idleCheckInterval = Random.Range(5f, 12f);
        _idleCheckTimer = _idleCheckInterval;
    }
}
