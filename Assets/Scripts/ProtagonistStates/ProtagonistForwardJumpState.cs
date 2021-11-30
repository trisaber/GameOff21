using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistForwardJumpState : ProtagonistStateBase
{
    [SerializeField] private float jumpSpeed = 2.5f;
    [SerializeField] private float jumpSpeedWhileStanding = 2.5f;
    [SerializeField] private float jumpSpeedWhileRunning = 4.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        canMoveWithoutInput = true;
        GetCharacterController(_animator);

        jumpSpeed = animator.GetFloat("speed") > 0.6 ? jumpSpeedWhileRunning : jumpSpeedWhileStanding;
        protagonist.state = this;
        protagonist.gravityActive = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ChangeState(protagonist.falling, animator, ProtagonistStates.Falling);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void StartOfAnimation()
    {
    }

    public override void StartOfAction()
    {
        protagonist.moveSpeed = jumpSpeed;
        protagonist.gravityActive = false;
    }

    public override void EndOfAction()
    {
        protagonist.gravityActive = true;
        protagonist.moveSpeed = 0.0f;
    }

    public override void EndOfAnimation()
    {
        ChangeState(animator, ProtagonistStates.OnGround);
        protagonist.gravityActive = true;
    }

}
