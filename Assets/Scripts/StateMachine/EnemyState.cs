using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        anim = enemy.anim;
        rb = enemy.rb;
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();

        float battleAnimSpeedMultiplier = enemy.battleMoveSpeed / enemy.moveSpeed;

        anim.SetFloat("BattleAnimSpeedMultiplier", battleAnimSpeedMultiplier);
        anim.SetFloat("MoveAnimSpeedMultiplier", enemy.moveAnimSpeedMultiplier);
        anim.SetFloat("XVelocity", rb.linearVelocity.x);
    }
}
