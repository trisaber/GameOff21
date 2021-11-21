using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistHangingState : ProtagonistStateBase
{
    public bool changeHanging = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        changeHanging = false;
        animator.SetFloat("hanging", 0.0f);
        GetCharacterController(animator);

        protagonist.state = this;
        protagonist.moveSpeed = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (changeHanging)
        {
            animator.SetFloat("hanging", 1.0f, 0.2f, Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            ChangeState(animator, ProtagonistStates.ClimbFromHanging);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            ChangeState(animator, ProtagonistStates.OnGround); // ProtagonistStates.FallFromHanging
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void EndOfAnimation()
    {
        changeHanging = true;
    }
}
