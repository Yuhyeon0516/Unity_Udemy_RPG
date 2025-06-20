using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public PlayerInput_Actions input { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerBasicAttackState basicAttackState { get; private set; }
    public PlayerJumpAttackState jumpAttackState { get; private set; }

    [Header("Attack Details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = 0.1f;
    public float comboResetTime = 1f;
    private Coroutine queuedAttackCoroutine;

    [Header("Movement Details")]
    public float moveSpeed;
    public float jumpForce = 5;
    public Vector2 wallJumpForce;
    [Range(0, 1)]
    public float inAirMoveMultiplier = 0.7f;
    [Range(0, 1)]
    public float wallSlideSlowMultiplier = 0.4f;
    [Space]
    public float dashDuration = 0.25f;
    public float dashSpeed = 20f;

    public Vector2 moveInput { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInput_Actions();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "JumpFall");
        fallState = new PlayerFallState(this, stateMachine, "JumpFall");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "JumpFall");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        basicAttackState = new PlayerBasicAttackState(this, stateMachine, "BasicAttack");
        jumpAttackState = new PlayerJumpAttackState(this, stateMachine, "JumpAttack");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        input.Player.Movement.canceled += context => moveInput = Vector2.zero;
    }

    void OnDisable()
    {
        input.Disable();
    }

    public void EnterAttackStateWithDelay()
    {
        if (queuedAttackCoroutine != null)
        {
            StopCoroutine(queuedAttackCoroutine);
        }

        queuedAttackCoroutine = StartCoroutine(EnterAttackStateWithDelayCoroutine());
    }

    private IEnumerator EnterAttackStateWithDelayCoroutine()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }
}
