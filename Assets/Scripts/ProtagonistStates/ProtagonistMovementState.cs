using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistMovementState : ProtagonistStateBase
{
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float runSpeed = 3.0f;
    private float walkStart = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterController(animator);
        animator.SetFloat("speed", 0.0f);
        protagonist.state = this;

        Input.ResetInputAxes();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Jump(animator) == false && Crouch(animator) == false)
        {
            Move(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    bool Crouch(Animator animator)
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            animator.SetInteger("animationId", (int)ProtagonistStates.Crouch);
            return true;
        }

        return false;
    }

    bool Jump(Animator animator)
    {
        if (Input.GetButtonDown("Jump"))
        {
            var aCollision = protagonist.CheckLedgeCollide();
            protagonist.jumpingCollider = aCollision;
            animator.SetInteger("animationId", (aCollision != null ? (int)ProtagonistStates.JumpGrabLedge : (int)ProtagonistStates.JumpMissedGrab));
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ChangeState(animator, ProtagonistStates.JumpForward);
            return true;
        }

        return false;
    }

    void Move(Animator animator)
    {
        var horizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontal) >= 0.15f)
        {
            if (walkStart == 0.0f)
            {
                walkStart = Time.time;
            }

            if (Time.time - walkStart < 3.0f)
            {
                protagonist.moveSpeed = walkSpeed;
                animator.SetFloat("speed", 0.5f, 0.1f, Time.deltaTime);
            }
            else
            {
                protagonist.moveSpeed = runSpeed;
                animator.SetFloat("speed", 1.0f, 0.1f, Time.deltaTime);
            }
        }
        else
        {
            walkStart = 0.0f;
            protagonist.moveSpeed = 0.0f;
            animator.SetFloat("speed", 0.0f, 0.2f, Time.deltaTime);
        }

    }
}
