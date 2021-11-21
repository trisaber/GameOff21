using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistClimbUpState : ProtagonistStateBase
{
    private bool isClimbed = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isClimbed = false;
        protagonist.state = this;
        GetCharacterController(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isClimbed)
        {
            isClimbed = false;
            protagonist.Climb();
            ChangeState(animator, ProtagonistStates.OnGround);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void EndOfAnimation()
    {
        isClimbed = true;
    }
}
