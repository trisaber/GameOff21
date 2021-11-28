using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistMovementState : ProtagonistStateBase
{
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float runSpeed = 3.0f;
    [SerializeField] private float timeToRun = 2.0f;
    private float walkStart = 0.0f;
    private bool endOfAnimation = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GOLog.Log();
        endOfAnimation = false;
        GetCharacterController(_animator);
        _animator.SetFloat("speed", 0.0f);
        _animator.SetInteger("animationId", 0);
        protagonist.state = this;
        protagonist.moveSpeed = 0.0f;
        protagonist.ResetTargetLedge();

        Input.ResetInputAxes();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GOLog.Log();

        if (endOfAnimation == false && Jump(_animator) == false && Crouch(_animator) == false)
        {
            Move(_animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GOLog.Log();
    }

    public override void EndOfAnimation()
    {
        endOfAnimation = true;
    }

    bool Crouch(Animator _animator)
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            _animator.SetInteger("animationId", (int)ProtagonistStates.Crouch);
            return true;
        }

        return false;
    }

    bool Jump(Animator _animator)
    {
        if (Input.GetButtonDown("Jump"))
        {
            var collidedLedge = CheckLedgeCollide();
            protagonist.targetLedge = collidedLedge != null ? collidedLedge.transform.position : protagonist.targetLedge;
            _animator.SetInteger("animationId", (collidedLedge != null ? (int)ProtagonistStates.JumpGrabLedge : (int)ProtagonistStates.JumpMissedGrab));
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ChangeState(_animator, ProtagonistStates.JumpForward);
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

            if (Time.time - walkStart < timeToRun)
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

    public Collider CheckLedgeCollide()
    {
        //if(Physics.CheckSphere(context.ledgeChecker.position, 0.15f, context.ledgeLayer))
        float force = walkStart > 0.0f ? protagonist.moveSpeed * protagonist.moveSpeed : 1.0f;
        Collider[] colliders = Physics.OverlapSphere(protagonist.getLedgeChecker().position, 0.15f * force, protagonist.getLedgeLayer());
        Transform model = protagonist.model;

        foreach (Collider c in colliders)
        {
            if (model.rotation.y > 0 && c.name == "LeftLedge" && model.position.x <= (c.transform.position.x + 0.15f))
            {
                // Debug.Log("Right ledge grabbed. pos.x: " + c.transform.position.x + ", diff x: " + (c.transform.position.x - context.ledgeChecker.position.x));
                // Debug.Log("ledge checker.x: " + context.ledgeChecker.transform.position.x + ", pos.x: " + context.transform.position.x);
                return c;
            }
            if (model.rotation.y < 0 && c.name == "RightLedge" && model.position.x >= (c.transform.position.x - 0.15f))
            {
                // Debug.Log("Right ledge grabbed. pos.x: " + c.transform.position.x + ", diff x: " + (c.transform.position.x - context.ledgeChecker.position.x));
                // Debug.Log("ledge checker.x: " + context.ledgeChecker.transform.position.x + ", pos.x: " + context.transform.position.x);
                return c;
            }
        }

        return null;
    }


}
