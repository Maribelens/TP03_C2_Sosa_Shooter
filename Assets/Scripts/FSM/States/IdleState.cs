using UnityEngine;

public class IdleState : StateBase
{
    private float _idleDuration;
    private float _elapsed;
    public IdleState() => enemyStateType = EnemyStateType.Idle;
    public override void OnEnter()
    {
        Animator.SetInteger(StateHash, (int)EnemyStateType.Idle);
        _elapsed = 0f;
        _idleDuration = Random.Range(2f, 5f);
    }

    public override void OnUpdate()
    {
        //Debug.Log($"IdleState Update - PlayerInRange: {Enemy.Detector.PlayerInRange}");
        if (Enemy.Detector.PlayerInRange)
        {
            FSM.ChangeState(EnemyStateType.Aiming);
            return;
        }
        
        _elapsed += Time.deltaTime;
        if (_elapsed >= _idleDuration)
            FSM.ChangeState(EnemyStateType.Walking);
    }
}
