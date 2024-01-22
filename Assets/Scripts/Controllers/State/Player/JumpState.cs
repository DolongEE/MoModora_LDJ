using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovingState
{
    public JumpState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }
}
