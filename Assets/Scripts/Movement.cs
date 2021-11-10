using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    #region configuration prarameters
    [Header("Player")]
    public CharacterController controller;
    private Vector3 direction;
    [SerializeField] float Movespeed = 10f;
    [SerializeField] float gravity = -20f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask ledgeLayer;
    [SerializeField] Transform ledgeChecker;

    [SerializeField] bool isHanging = false;
    [SerializeField] bool isGrounded;
    public Animator animator;
    public Transform model;
    public float deltaX;

    PlayerAbstractState state;


    #endregion

    void Start()
    {
        TransitionTo(new PlayerMovementState());
    }

    void FixedUpdate()
    {
        state.Move();
        state.Jump();

        controller.Move(direction);
    }

    void Update2()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        if (isGrounded & !isHanging)  //checks if the player is grounded
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isGrounded", true);
            direction.y = 0;
            Move();
            Jump();
        }
        else if (isHanging)
        {
            direction.y = 0;
            direction.x = 0;
            Fall();
            Climb();
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
        controller.Move(direction);
    }
    private void Climb()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isHanging = false;
            Jump();
        }
    }
    private void Fall()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isHanging = false;
            direction.y += gravity * Time.deltaTime;
        }
    }

    private void Move()
    {
        deltaX = Input.GetAxis("Horizontal");
        direction.x = deltaX * Movespeed * Time.deltaTime;

        animator.SetFloat("speed", Mathf.Abs(deltaX));// spped run animationı çalıştırıyor.
        if (deltaX != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(deltaX, 0, 0));
            model.rotation = newRotation;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isGrounded", false);
            //direction.y = JumpForce * Time.deltaTime;
            isHanging = Physics.CheckSphere(ledgeChecker.position, 0.15f, ledgeLayer);
            animator.SetBool("isHanging", isHanging);
            animator.SetBool("isJump", true);
        }

    }
    public void Hanged()
    {
        isHanging = true;
        animator.SetBool("isHanging", isHanging);
    }

    private void Climbing()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
    }

    public void Climbed()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        isHanging = false;
        animator.SetBool("isHanging", isHanging);
        animator.SetBool("isJump", false);
    }

    private void TransitionTo(PlayerAbstractState newState)
    {
        state = newState;
        state.SetContext(this);
        state.SetAnimatorStates();
    }


    private abstract class PlayerAbstractState
    {
        protected Movement context;

        public void SetContext(Movement _context)
        {
            this.context = _context;
        }

        // Start is called before the first frame update
        public abstract void Move();
        public abstract void Jump();
        public abstract void SetAnimatorStates();

        protected void SetAnimatorStates(bool isGrounded, bool isHanging, bool isJump)
        {
            context.animator.SetBool("isGrounded", isGrounded);
            context.animator.SetBool("isHanging", isHanging);
            context.animator.SetBool("isJump", isJump);

        }
    }

    private class PlayerMovementState : PlayerAbstractState
    {
        private bool doesCollide()
        {
            bool result = false;

            if(Physics.CheckSphere(context.ledgeChecker.position, 0.15f, context.ledgeLayer))
            {
                Collider[] colliders = Physics.OverlapSphere(context.ledgeChecker.position, 0.15f, context.ledgeLayer);
                foreach(Collider c in colliders)
                {
                    if (context.model.rotation.y > 0 && c.name == "LeftLedge")
                    {
                        result = true;
                    }
                    if(context.model.rotation.y < 0 && c.name == "RightLedge")
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public override void Jump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (doesCollide())
                {
                    context.TransitionTo(new PlayerMovementJumpState());
                }
                else
                {
                    context.TransitionTo(new PlayerMovementJumpAndFallState());
                }
            }
        }

        public override void Move()
        {
            JumpAndFallAnimation jafa = context.animator.GetBehaviour<JumpAndFallAnimation>();
            ClimbAnimation ca = context.animator.GetBehaviour<ClimbAnimation>();
            if (jafa.inAnimation == false && ca.inAnimation == false)
            {
                context.Move();
            }
        }

        public override void SetAnimatorStates()
        {
            Debug.Log("PlayerMovementState::SetAnimatorStates");

            SetAnimatorStates(true, false, false);
        }
    }


    private class PlayerMovementJumpState : PlayerAbstractState
    {
        public override void Jump()
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                context.TransitionTo(new PlayerMovementClimbState());
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                context.TransitionTo(new PlayerMovementFallState());
            }
        }

        public override void Move()
        {
            context.direction.Set(0, 0, 0);
        }

        public override void SetAnimatorStates()
        {
            Debug.Log("PlayerMovementJumpState::SetAnimatorStates");

            SetAnimatorStates(false, true, true);
        }
    }

    private class PlayerMovementJumpAndFallState : PlayerAbstractState
    {
        public override void Jump()
        {
        }

        public override void Move()
        {
            context.direction.Set(0, 0, 0);
            if (context.animator.GetBehaviour<JumpAndFallAnimation>().inAnimation)
            {
                context.TransitionTo(new PlayerMovementState());
            }
        }

        public override void SetAnimatorStates()
        {
            Debug.Log("PlayerMovementJumpAndFallState::SetAnimatorStates");

            SetAnimatorStates(false, false, true);
        }
    }

    private class PlayerMovementClimbState : PlayerAbstractState
    {
        public override void Jump()
        {
        }

        public override void Move()
        {
            context.direction.Set(0, 0, 0);
            if( context.animator.GetBehaviour<ClimbAnimation>().inAnimation) {
                context.Climbing();
                context.TransitionTo(new PlayerMovementState());
            }
        }

        public override void SetAnimatorStates()
        {
            Debug.Log("PlayerMovementClimbState::SetAnimatorStates");

            SetAnimatorStates(false, true, false);
        }
    }

    private class PlayerMovementFallState : PlayerAbstractState
    {
        public override void Jump()
        {
        }

        public override void Move()
        {
            context.direction.Set(0, 0, 0);
            if (context.animator.GetBehaviour<ClimbAnimation>().inAnimation)
            {
                context.TransitionTo(new PlayerMovementState());
            }
        }

        public override void SetAnimatorStates()
        {
            Debug.Log("PlayerMovementClimbState::SetAnimatorStates");

            SetAnimatorStates(false, true, false);
        }
    }

}

