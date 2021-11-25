using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistCrouchState : ProtagonistStateBase
{
    [SerializeField] private float walkSpeed = 1.0f;
    private bool transitionToIdle = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterController(animator);
        animator.SetFloat("speed", 0.0f);
        protagonist.state = this;
        protagonist.moveSpeed = 0.0f;

        Input.ResetInputAxes();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (transitionToIdle)
        {
            animator.SetFloat("speed", 0.5f);
            transitionToIdle = false;
        }

        if (Jump(animator) == false && Up(animator) == false)
        {
            Move(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    bool Up(Animator animator)
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            ChangeState(animator, ProtagonistStates.OnGround);
            return true;
        }

        return false;
    }

    bool Jump(Animator animator)
    {
        if (Input.GetButtonDown("Jump"))
        {
            ChangeState(animator, ProtagonistStates.MayaUkemi);
            return true;
        }

        return false;
    }


    void Move(Animator animator)
    {
        var horizontal = Input.GetAxis("Horizontal");
        Debug.Log("Mathf.Abs(horizontal): " + Mathf.Abs(horizontal));
        if (Mathf.Abs(horizontal) >= 0.15f)
        {
            protagonist.moveSpeed = walkSpeed;
            animator.SetFloat("speed", 1.0f, 0.15f, Time.deltaTime);
        }
        else
        {
            protagonist.moveSpeed = 0.0f;
            animator.SetFloat("speed", (transitionToIdle ? 0.0f : 0.5f), 0.2f, Time.deltaTime);
        }

    }

    public override void EndOfAnimation()
    {
        transitionToIdle = true;
    }

}
