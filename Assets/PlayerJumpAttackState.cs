public class PlayerJumpAttackState : EntityState
{
    private bool touchedGround;

    public PlayerJumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        touchedGround = false;
        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDir, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.groundDetected && !touchedGround)
        {
            touchedGround = true;
            anim.SetTrigger("JumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocity.y);
            return;
        }

        if (triggerCalled && player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
