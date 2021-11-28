using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistJumpingState : ProtagonistStateBase
{
    [SerializeField] private float movingFactor = 1.2f;// to tune the jump action

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        canMoveWithoutInput = true;
        GetCharacterController(_animator);

        protagonist.state = this;
        protagonist.moveSpeed = Mathf.Abs(protagonist.targetLedge.x - protagonist.transform.position.x) * movingFactor;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float diffX = protagonist.targetLedge.x - protagonist.transform.position.x;
        if (Mathf.Abs(diffX) <= 0.01)
        {
            protagonist.ResetTargetLedge();
            protagonist.moveSpeed = 0.0f;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        protagonist.ResetTargetLedge();
        protagonist.moveSpeed = 0.0f;
    }

    public override void EndOfAnimation()
    {
    }
}
        