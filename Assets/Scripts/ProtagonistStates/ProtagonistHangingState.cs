﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistHangingState : ProtagonistStateBase
{
    public bool changeToHangingIdle = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterController(_animator);
        changeToHangingIdle = false;
        _animator.SetFloat("hanging", 0.0f);

        protagonist.state = this;
        protagonist.moveSpeed = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (changeToHangingIdle)
        {
            _animator.SetFloat("hanging", 1.0f, 0.2f, Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            ChangeState(_animator, ProtagonistStates.ClimbFromHanging);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            ChangeState(_animator, ProtagonistStates.OnGround); // ProtagonistStates.FallFromHanging
        }

        float diffX = protagonist.targetLedge.x - protagonist.transform.position.x;
        if (Mathf.Abs(diffX) <= 0.01)
        {
            protagonist.targetLedge = Vector3.zero;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void EndOfAnimation()
    {
        changeToHangingIdle = true;
    }
}
