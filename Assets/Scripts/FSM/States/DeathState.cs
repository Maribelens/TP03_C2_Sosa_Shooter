using UnityEngine;

public class DeathState : StateBase
{
    //public virtual void Initialize(Animator animator, FSMManager fsm, EnemyController enemy)
    //{
    //    base.Initialize(animator, fsm, enemy);
    //    enemyStateType = EnemyStateType.Death;
    //}
    
    public DeathState() => enemyStateType = EnemyStateType.Death;
    public override void OnEnter()
    {
        Animator.SetInteger(StateHash, (int)EnemyStateType.Death);
        // EnemyBase maneja Destroy y partículas
    }
}
