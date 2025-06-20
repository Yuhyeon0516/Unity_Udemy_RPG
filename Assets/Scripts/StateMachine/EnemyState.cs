public class EnemyState : EntityState
{
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        anim = enemy.anim;
        rb = enemy.rb;
    }

    public override void Update()
    {
        base.Update();

        anim.SetFloat("MoveAnimSpeedMultiplier", enemy.moveAnimSpeedMultiplier);
    }
}
