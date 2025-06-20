
using UnityEngine;

public class PlayerBasicAttackState : PlayerState
{
    private float attackVelocityTimer;

    private const int FirstComboIndex = 1;
    private int attackDir;
    private int comboIndex = 1;
    private int comboLimit = 3;
    private bool comboAttackQueued;

    private float lastTimeAttacked;

    public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if (comboLimit != player.attackVelocity.Length)
        {
            comboLimit = player.attackVelocity.Length;
            Debug.LogWarning("I've adjusted combo limit, according to attack velocity array");
        }
    }

    public override void Enter()
    {
        base.Enter();

        comboAttackQueued = false;

        ResetComboIndexIfNeeded();

        if (player.moveInput.x != 0)
        {
            attackDir = (int)(player.moveInput.x);
        }
        else
        {
            attackDir = player.facingDir;
        }

        anim.SetInteger("BasicAttackIndex", comboIndex);

        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();

        if (input.Player.Attack.WasPressedThisFrame())
        {
            QueueNextAttack();
        }

        HandleStateExit();
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex++;

        lastTimeAttacked = Time.time;
    }

    private void HandleStateExit()
    {
        if (triggerCalled)
        {
            if (comboAttackQueued)
            {
                anim.SetBool(animBoolName, false);
                player.EnterAttackStateWithDelay();
            }
            else
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
        {
            comboAttackQueued = true;
        }
    }

    private void ResetComboIndexIfNeeded()
    {
        if (Time.time > lastTimeAttacked + player.comboResetTime)
        {
            comboIndex = FirstComboIndex;
        }

        if (comboIndex > comboLimit)
        {
            comboIndex = FirstComboIndex;
        }
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
        {
            player.SetVelocity(0, rb.linearVelocity.y);
        }
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
    }
}
