using UnityEngine;

public class HurtState : StateBase
{
    private float _hurtDuration = 0.5f;
    private float _elapsed;

    //private EnemyStateType _previousState;
    public HurtState() => enemyStateType = EnemyStateType.Hurt;

    public override void OnEnter()
    {
        Animator.SetInteger(StateHash, (int)EnemyStateType.Hurt);
        _elapsed = 0f;
        //_previousState = FSM.CurrentState?.enemyStateType?? EnemyStateType.Idle;
    }

    public override void OnUpdate()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed >= _hurtDuration)
        {
            EnemyStateType previous = FSM.PreviousState != EnemyStateType.None
                ? FSM.PreviousState
                : EnemyStateType.Idle;

            FSM.ChangeState(previous);
        }
    }
}
