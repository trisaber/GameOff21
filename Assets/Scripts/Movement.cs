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

    public Animator animator;
    public Transform model;
    public float deltaX;

    #endregion
    // member variables
    public PlayerAbstractState state { get; private set; } = null;

    void Start()
    {
        TransitionTo(new PlayerMovementState());
    }

    void FixedUpdate()
    {
        state.Jump();
        state.Move();

        controller.Move(direction);
        TransitionTo(state.nextState);
    }

    private void Move()
    {
        // if there is any emptiness, fall down
        if (!Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer))
        {
            direction.y += gravity * Time.deltaTime;
        }
        else  // character can move freely on ground
        {
            deltaX = Input.GetAxis("Horizontal");
            direction.x = deltaX * Movespeed * Time.deltaTime;

            animator.SetFloat("speed", Mathf.Abs(deltaX));// speed run animationı çalıştırıyor.
            if (deltaX != 0)
            {
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(deltaX, 0, 0));
                model.rotation = newRotation;
            }
        }
    }

    private void logPosition(string s)
    {
        Debug.Log(s + " [transform position: " + transform.position + "]");
    }

    public void Climb()
    {
        controller.enabled = false;
        transform.position += new Vector3(0, 3, 0);
        controller.enabled = true;
    }

    private void TransitionTo(PlayerAbstractState newState)
    {
        if (newState != null) {
            state = newState;
            state.SetContext(this);
            state.SetAnimatorStates();
        }
    }


    // #################### -------------------- ####################
    // State machine inner classes are below
    // #################### -------------------- ####################
    public abstract class PlayerAbstractState
    {
        public PlayerAbstractState nextState { get; set; } = null;
        protected Movement context;

        // abstract methods
        // Start is called before the first frame update
        public abstract void Move();
        public abstract void Jump();
        public abstract void SetAnimatorStates();

        // virtual methods
        public virtual void EndOfAnimation() {}

        public void SetContext(Movement _context)
        {
            this.context = _context;
        }

        protected void SetAnimatorStates(bool isGrounded, bool isHanging, bool isJump)
        {
            context.animator.SetBool("isGrounded", isGrounded);
            context.animator.SetBool("isHanging", isHanging);
            context.animator.SetBool("isJump", isJump);
        }

    }

    private class PlayerMovementState : PlayerAbstractState
    {
        private Collider doesCollide()
        {
            //if(Physics.CheckSphere(context.ledgeChecker.position, 0.15f, context.ledgeLayer))
            Collider[] colliders = Physics.OverlapSphere(context.ledgeChecker.position, 2 /*0.15f * (1 + 4 * context.animator.GetFloat("speed"))*/, context.ledgeLayer);
            foreach(Collider c in colliders)
            {
                if (context.model.rotation.y > 0 && c.name == "LeftLedge" && context.model.position.x <= (c.transform.position.x + 0.15f))
                {
                    // Debug.Log("Right ledge grabbed. pos.x: " + c.transform.position.x + ", diff x: " + (c.transform.position.x - context.ledgeChecker.position.x));
                    // Debug.Log("ledge checker.x: " + context.ledgeChecker.transform.position.x + ", pos.x: " + context.transform.position.x);
                    return c;
                }
                if(context.model.rotation.y < 0 && c.name == "RightLedge"&& context.model.position.x >= (c.transform.position.x - 0.15f))
                {
                    // Debug.Log("Right ledge grabbed. pos.x: " + c.transform.position.x + ", diff x: " + (c.transform.position.x - context.ledgeChecker.position.x));
                    // Debug.Log("ledge checker.x: " + context.ledgeChecker.transform.position.x + ", pos.x: " + context.transform.position.x);
                    return c;
                }
            }
            
            return null;
        }

        public override void Jump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                var aCollision = doesCollide();
                if (aCollision != null)
                {
                    this.nextState = new PlayerJumpAndHangState(aCollision.transform.position - context.ledgeChecker.position);
                }
                else
                {
                    this.nextState = new PlayerJumpAndFallState();
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

    /**
     * Animation: Armature_jump_up
     */
    private class PlayerJumpAndHangState : PlayerAbstractState
    {
        Vector3 grabbedLedgeDistance;
        Vector3 speed;
        public PlayerJumpAndHangState(Vector3 _diff)
        {
            grabbedLedgeDistance = _diff;
            speed = grabbedLedgeDistance / 40;
            Debug.Log("diff vector: " + _diff + ", speed vector: " + speed);
        }

        public override void Jump()
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                this.nextState = new PlayerClimbState();
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                this.nextState = new PlayerFallFromHangState();
            }
        }

        public override void Move()
        {
            if (grabbedLedgeDistance != Vector3.zero)
            {
                // Debug.Log("debug jumping start=========================================================================");
                // Debug.Log("grabbedLedgeDistance: " + grabbedLedgeDistance.x + ", pos.x: " + context.transform.position.x + ", speed.x: " + speed.x);
                grabbedLedgeDistance -= speed;
                context.controller.Move(speed);
                // Debug.Log("grabbedLedgeDistance: " + grabbedLedgeDistance.x + ", pos.x: " + context.transform.position.x);
                // Debug.Log("debug jumping end . . . . . . . . . . . . . . . . . . . . .  ..  .. . . . . . . . . .. . . ");
            }

            context.direction.Set(0, 0, 0);
        }

        public override void SetAnimatorStates()
        {
            Debug.Log("PlayerMovementJumpState::SetAnimatorStates");

            SetAnimatorStates(false, true, true);
        }
    }
    
    private class PlayerJumpAndFallState : PlayerAbstractState
    {
        public override void Jump()
        {
        }

        public override void Move()
        {
            context.direction.Set(0, 0, 0);
            // if (!context.animator.GetBehaviour<JumpAndFallAnimation>().inAnimation) {}
        }

        public override void EndOfAnimation()
        {
            Debug.Log("PlayerMovementJumpAndFallState::EndOfAnimation: set next state to PlayerMovement");
            this.nextState = new PlayerMovementState();
        }

        public override void SetAnimatorStates()
        {
            context.logPosition("PlayerMovementJumpAndFallState::SetAnimatorStates");

            SetAnimatorStates(false, false, true);
        }
    }

    private class PlayerClimbState : PlayerAbstractState
    {
        public override void Jump() {}

        public override void Move()
        {
            context.direction.Set(0, 0, 0);
            // if(!context.animator.GetBehaviour<ClimbAnimation>().inAnimation) {}
        }

        public override void EndOfAnimation()
        {
            Debug.Log("PlayerMovementClimbState::EndOfAnimation: set next state to PlayerMovement");
            context.Climb();
            this.nextState = new PlayerMovementState();
        }

        public override void SetAnimatorStates()
        {
            Debug.Log("PlayerMovementClimbState::SetAnimatorStates");

            SetAnimatorStates(false, true, false);
        }
    }

    private class PlayerFallFromHangState : PlayerAbstractState
    {
        public override void Jump() {}

        public override void Move()
        {
            context.direction.Set(0, 0, 0);
            if (!context.animator.GetBehaviour<ClimbAnimation>().inAnimation)
            {
                Debug.Log("PlayerMovementFallState::Move");
                this.nextState = new PlayerMovementState();
            }
        }

        public override void EndOfAnimation()
        {
            Debug.Log("PlayerMovementClimbState::EndOfAnimation: set next state to PlayerMovement");
            this.nextState = new PlayerMovementState();
        }

        public override void SetAnimatorStates()
        {
            Debug.Log("PlayerMovementClimbState::SetAnimatorStates");

            SetAnimatorStates(false, true, false);
        }
    }

/*
    [SerializeField] bool isHanging = false;
    [SerializeField] bool isGrounded;

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

        public void Climbed()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        isHanging = false;
        animator.SetBool("isHanging", isHanging);
        animator.SetBool("isJump", false);
    }
    
    public void Climbed()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        isHanging = false;
        animator.SetBool("isHanging", isHanging);
        animator.SetBool("isJump", false);
    }

// */

}

