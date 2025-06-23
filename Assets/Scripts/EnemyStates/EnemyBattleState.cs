using UnityEngine;

public class EnemyBattleState : EnemyState
{
    private Transform player;

    private float lastTimeWasInBattle;
    private float inGameTime;

    public EnemyBattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (player == null)
        {
            player = enemy.PlayerDtection().transform;
        }

        if (ShouldRetreat())
        {
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDtection())
        {
            UpdateBattleTimer();
        }

        if (BattleTimeIsOver())
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (WithInAttackRange() && enemy.PlayerDtection())
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
        }
    }

    private void UpdateBattleTimer()
    {
        lastTimeWasInBattle = Time.time;
    }

    private bool BattleTimeIsOver()
    {
        return Time.time > lastTimeWasInBattle + enemy.battleTimeDuration;
    }

    private bool ShouldRetreat()
    {
        return DistanceToPlayer() < enemy.minRetreatDistance;
    }

    private bool WithInAttackRange()
    {
        return DistanceToPlayer() < enemy.attackDistance;
    }

    private float DistanceToPlayer()
    {
        if (player == null)
        {
            return float.MaxValue;
        }

        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    private int DirectionToPlayer()
    {
        if (player == null)
        {
            return 0;
        }

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
