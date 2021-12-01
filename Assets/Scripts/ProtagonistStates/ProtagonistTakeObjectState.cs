using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistTakeObjectState : ProtagonistStateBase
{
    private Collector collector;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterController(_animator);
        protagonist.state = this;

        collector = protagonist.GetComponentInParent<Collector>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void StartOfAction()
    {
        collector.PickUp();
    }

    public override void EndOfAnimation()
    {
        if (!collector.isLadybug())
        {
            ChangeState(animator, ProtagonistStates.OnGround);
        }
    }

}
