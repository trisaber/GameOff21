using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistFallingState : ProtagonistStateBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterController(_animator);

        protagonist.state = this;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var ground = GroundFinder.NextPlatformBeneath(protagonist.getGroundChecker().position);
        ChangeState((protagonist.getGroundChecker().position.y - ground.y < 0.8), _animator, ProtagonistStates.FinalFall);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void EndOfAnimation()
    {
                animator.Play("Armature|lethal_fall_70");
/*
        var ac = animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        foreach (var temp in ac.layers[0].stateMachine.states)
        {
            GOLog.Log(true, temp.state.name);
            if (temp.state.name == "Armature|lethal_fall_70")
            {
                GOLog.Log(true, "found the searched state");

            }
        }
*/
    }
}
