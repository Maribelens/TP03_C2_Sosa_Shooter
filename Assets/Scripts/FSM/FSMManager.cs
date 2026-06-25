using System.Collections.Generic;
using UnityEngine;

public class FSMManager : MonoBehaviour
{
    private Animator _animator;
    private List<StateBase> _states;

    public StateBase CurrentState { get; private set; }
    public StateBase PreviousState { get; private set; }

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

        StateBase next = FindState(nextStateType);
        if (next == null) return;

        CurrentState?.OnExit();
        PreviousState = CurrentState;
        CurrentState = next;
        CurrentState.OnEnter();
    }

    public StateBase FindState(EnemyStateType stateType)
    {
        return _states.Find(s => s.enemyStateType == stateType);
    }
}

