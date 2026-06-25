using UnityEngine;

public abstract class StateBase
{
    protected static readonly int StateHash = Animator.StringToHash("State");

    protected Animator Animator;
    protected FSMManager FSM;
    protected EnemyController Enemy;

    public EnemyStateType enemyStateType = EnemyStateType.None;

    public virtual void Initialize(Animator animator, FSMManager fsm, EnemyController enemy)
    {
        Animator = animator;
        FSM = fsm;
        Enemy = enemy;
    }

    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}
