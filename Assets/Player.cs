using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;

    private EntityState idleState;

    void Awake()
    {
        stateMachine = new StateMachine();

        idleState = new EntityState(stateMachine, "Idle State");
    }

    void Start()
    {
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.currentState.Update();
    }
}
