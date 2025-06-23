public class EnemySkeleton : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        moveState = new EnemyMoveState(this, stateMachine, "Move");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }
}
