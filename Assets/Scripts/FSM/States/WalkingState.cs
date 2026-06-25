using UnityEngine;
using UnityEngine.AI;

public class WalkingState : StateBase
{
    //public virtual void Initialize(Animator animator, FSMManager fsm, EnemyController enemy)
    //{
    //    base.Initialize(animator, fsm, enemy);
    //    enemyStateType = EnemyStateType.Walking;
    //}
    
    public WalkingState() => enemyStateType = EnemyStateType.Walking;

    public override void OnEnter()
    {
        Animator.SetInteger(StateHash, (int)EnemyStateType.Walking);
        Enemy.Agent.isStopped = false;
        SetRandomDestination();
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
    }

    public override void OnExit()
    {
        Enemy.Agent.isStopped = true;
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * Enemy.PatrolRadius;
        randomDirection += Enemy.transform.position;

        if(NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, Enemy.PatrolRadius, NavMesh.AllAreas))
        Enemy.Agent.SetDestination(hit.position); 
    }
}
