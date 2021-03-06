using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistHangingState : ProtagonistStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterController(_animator);

        protagonist.state = this;
        protagonist.moveSpeed = 0.0f;
        protagonist.ResetTargetLedge();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            ChangeState(_animator, ProtagonistStates.ClimbFromHanging);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            ChangeState(_animator, ProtagonistStates.OnGround); // ProtagonistStates.FallFromHanging
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void EndOfAnimation()
    {
    }
}
