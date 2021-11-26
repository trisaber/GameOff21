using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistClimbUpState : ProtagonistStateBase
{
    private bool endOfAnimation = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterController(_animator);
        protagonist.state = this;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (endOfAnimation)
        {
            protagonist.climbDirection = 1;
            protagonist.Climb();
            endOfAnimation = false;
        }
    }

    public override void EndOfAnimation()
    {
        endOfAnimation = true;
        ChangeState(animator, ProtagonistStates.OnGround);
    }
}
