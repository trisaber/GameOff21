﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistGenericState : ProtagonistStateBase
{
    [SerializeField] private float speed;
    [SerializeField] private ProtagonistStates nextState;
    Animator animator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterController(animator);
        this.animator = animator;
        protagonist.state = this;

        protagonist.moveSpeed = speed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void EndOfAnimation()
    {
        animator.SetInteger("animationId", (int)nextState);
    }

}