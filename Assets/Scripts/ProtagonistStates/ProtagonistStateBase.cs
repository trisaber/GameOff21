using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistStateBase : StateMachineBehaviour
{
    public enum ProtagonistStates
    {
        OnGround = 0,
        JumpGrabLedge = 1,
        JumpMissedGrab = 2,
        JumpForward = 3,
        HangingAtLedge = 4,
        FallFromHanging = 5,
        ClimbFromHanging = 6,
        Crouch = 7,
        MayaUkemi = 8,
    }

    public bool canChangeDirection = false;

    protected CharacterController cc = null;
    protected Protagonist protagonist;
    protected Animator animator;

    protected CharacterController GetCharacterController(Animator _animator)
    {
        if (cc == null && _animator != null)
        {
            animator = _animator;
            cc = animator.GetComponentInParent<CharacterController>();
            protagonist = animator.GetComponentInParent<Protagonist>();
            if (protagonist == null || cc == null)
            {
                Application.Quit();
            }
        }
        return cc;
    }

    public void ChangeState(Animator animator, ProtagonistStates nextState)
    {
        animator.SetInteger("animationId", (int)nextState);
    }

    // It is called when end frame of animation is reached.
    public virtual void EndOfAnimation() { }
    public virtual void StartOfAnimation() { }
}
