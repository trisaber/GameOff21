using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistGenericState : ProtagonistStateBase
{
    [SerializeField] private float speed;
    [SerializeField] private string name = "generic_state";
    [SerializeField] private ProtagonistStates nextState;
    [SerializeField] private string changeAnimationStateSpeed = "";

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // GOLog.ActivateClass(this.GetType());
        GOLog.Log("Animation" + name);

        canMoveWithoutInput = speed > 0;
        GetCharacterController(_animator);
        protagonist.state = this;

        protagonist.moveSpeed = speed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GOLog.Log("Animation" + name);

        CheckAnimationStateSpeed();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GOLog.Log("Animation" + name);
    }

    override public void EndOfAnimation()
    {
        ChangeState(animator, nextState);
    }


    private void CheckAnimationStateSpeed()
    {
        if (changeAnimationStateSpeed.Length > 0 && Input.anyKey)
        {
            var ac = animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
            GOLog.Log(true, "animator speed: " + ac.layers[0].stateMachine.defaultState.speed);
            foreach (var state in ac.layers[0].stateMachine.states)
            {
                if (state.state.name == changeAnimationStateSpeed) { 
                    state.state.speed = 1;
                }
            }
        }
    }
}
