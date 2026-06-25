using UnityEngine;

public class IdleState : StateBase
{
    //public virtual void Initialize(Animator animator, FSMManager fsm, EnemyController enemy)
    //{
    //    base.Initialize(animator, fsm, enemy);
    //    enemyStateType = EnemyStateType.Idle;
    //}

    public IdleState() => enemyStateType = EnemyStateType.Idle;
    public override void OnEnter()
    {
        Animator.SetInteger(StateHash, (int)EnemyStateType.Idle);
    }

    public override void OnUpdate()
    {
        Debug.Log($"IdleState Update - PlayerInRange: {Enemy.Detector.PlayerInRange}");
        if (Enemy.Detector.PlayerInRange)
            FSM.ChangeState(EnemyStateType.Aiming);
    }
}
