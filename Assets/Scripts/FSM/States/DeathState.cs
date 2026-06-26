public class DeathState : StateBase
{
    public DeathState() => enemyStateType = EnemyStateType.Death;
    public override void OnEnter()
    {
        Animator.SetInteger(StateHash, (int)EnemyStateType.Death);
        // EnemyBase maneja Destroy y partículas
    }
}
