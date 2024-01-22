using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class IdleState : State
{
    protected bool isJump;
    protected bool isAttack;
    protected bool isBow;
    protected bool isCrouch;
    protected bool isRoll;
    protected bool isMove;
    public IdleState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandlerInput()
    {
        base.HandlerInput();
        isAttack = Input.GetKeyDown(KeyCode.S);
        isBow = Input.GetKeyDown(KeyCode.D);
        isCrouch = Input.GetKeyDown(KeyCode.DownArrow);
        isJump = Input.GetKeyDown(KeyCode.A);
        isRoll = Input.GetKeyDown(KeyCode.Q);
        isMove = Input.GetButtonDown("Horizontal");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //if (isMove)
        //    stateMachine.ChangeState(player.movingState);
        //if (isCrouch)
        //    stateMachine.ChangeState(player.CrouchState);
        //if (isAttack)
        //    stateMachine.ChangeState(player.AttackState);
        //if (isBow)
        //    stateMachine.ChangeState(player.BowState);
        //if (isJump)
        //    stateMachine.ChangeState(player.JumpState);
        //if (isRoll)
        //    stateMachine.ChangeState(player.RollState);
    }
}
