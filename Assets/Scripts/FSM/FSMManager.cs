using System.Collections.Generic;
using UnityEngine;

public class FSMManager : MonoBehaviour
{
    private Animator _animator;
    private List<StateBase> _states;
    public StateBase CurrentState { get; private set; }
    public EnemyStateType PreviousState { get; private set; } = EnemyStateType.None;

    public void Initialize(Animator animator, EnemyController enemy, List<StateBase> states)
    {
        _animator = animator;
        _states = states;

        foreach (StateBase state in _states)
            state.Initialize(_animator, this, enemy);

        ChangeState(EnemyStateType.Walking);
    }
    public void Update()
    {
        CurrentState?.OnUpdate();
    }

    public void ChangeState(EnemyStateType nextStateType)
    {
        if (CurrentState != null && CurrentState.enemyStateType == EnemyStateType.Death) return;

        if (CurrentState != null && CurrentState.enemyStateType == EnemyStateType.Hurt && nextStateType == EnemyStateType.Hurt) return;

        //guardado de estado previo
        if (nextStateType == EnemyStateType.Hurt)
            PreviousState = CurrentState?.enemyStateType ?? EnemyStateType.Idle;

        StateBase next = FindState(nextStateType);
        if (next == null) return;

        CurrentState?.OnExit();
        CurrentState = next;
        CurrentState.OnEnter();
    }

    public StateBase FindState(EnemyStateType stateType)
    {
        return _states.Find(s => s.enemyStateType == stateType);
    }
}

