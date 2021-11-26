using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistGenericState : ProtagonistStateBase
{
    [SerializeField] private float speed;
    [SerializeField] private string name = "generic_state";
    [SerializeField] private ProtagonistStates nextState;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GOLog.Log();
        GetCharacterController(_animator);
        protagonist.state = this;

        protagonist.moveSpeed = speed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GOLog.Log();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GOLog.Log();
    }

    override public void EndOfAnimation()
    {
        ChangeState(animator, nextState);
    }

}
