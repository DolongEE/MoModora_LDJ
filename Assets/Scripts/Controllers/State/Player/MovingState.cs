using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State
{
    float speed;
    float horizontalInput;
    public MovingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        speed = player._moveSpeed;

    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void HandlerInput()
    {
        base.HandlerInput();
        horizontalInput = Input.GetAxis("Horizontal");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!Input.anyKey)
        {

        }
            
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
