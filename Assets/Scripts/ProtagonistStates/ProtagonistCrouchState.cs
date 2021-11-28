using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistCrouchState : ProtagonistStateBase
{
    [SerializeField] private float walkSpeed = 1.0f;
    private bool transitionToIdle = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetCharacterController(_animator);
        _animator.SetFloat("speed", 0.0f);
        protagonist.state = this;
        protagonist.moveSpeed = 0.0f;

        Input.ResetInputAxes();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (transitionToIdle)
        {
            _animator.SetFloat("speed", 0.5f);
            transitionToIdle = false;
        }

        if (Jump(_animator) == false && UpDown(_animator) == false)
        {
            Move(_animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator _animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void EndOfAnimation()
    {
        transitionToIdle = true;
    }

    private bool UpDown(Animator _animator)
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            ChangeState(_animator, ProtagonistStates.OnGround);
            return true;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            Collider[] colliders = Physics.OverlapSphere(protagonist.getGroundChecker().position, 0.15f, protagonist.getLedgeLayer());
            if(colliders.Length > 0)
            {
                if (colliders[0].name == "LeftLedge") { protagonist.TurnRight(); }
                else { protagonist.TurnLeft(); }
                ChangeState(_animator, ProtagonistStates.ClimbDown);
            }

            return true;
        }

       return false;
    }

    private bool Jump(Animator _animator)
    {
        if (Input.GetButtonDown("Jump"))
        {
            ChangeState(_animator, ProtagonistStates.MayaUkemi);
            return true;
        }

        return false;
    }


    private void Move(Animator _animator)
    {
        var horizontal = Input.GetAxis("Horizontal");
        Debug.Log("Mathf.Abs(horizontal): " + Mathf.Abs(horizontal));
        if (Mathf.Abs(horizontal) >= 0.15f)
        {
            protagonist.moveSpeed = walkSpeed;
            _animator.SetFloat("speed", 1.0f, 0.15f, Time.deltaTime);
        }
        else
        {
            protagonist.moveSpeed = 0.0f;
            _animator.SetFloat("speed", (transitionToIdle ? 0.0f : 0.5f), 0.2f, Time.deltaTime);
        }

    }

}
