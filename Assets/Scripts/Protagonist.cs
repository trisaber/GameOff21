using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Protagonist : MonoBehaviour
{
    #region configuration prarameters
    [Header("Protagonist")]
    public CharacterController controller;

    private Vector3 direction;
    public float moveSpeed = 0.0f;
    // [SerializeField] float maxMoveSpeed { get; } = 2.0f;
    [SerializeField] float acceleration { get; } = 0.1f;
    [SerializeField] float gravity = -20f;

    // layers
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask ledgeLayer;
    // physics checkers
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform ledgeChecker;

    public Animator animator;
    public Transform model;
    public Vector3 targetLedge;
    #endregion

    // member variables
    public ProtagonistStateBase state = null;
    public int climbDirection = 0;
    public bool gravityActive = true;
    public bool falling { get; private set; } = false;

    private GroundFinder groundFinder;

    private void Awake()
    {
        ResetTargetLedge();
    }

    private void Start()
    {
    }

    private void FixedUpdate() { }

    private void Update()
    {
        GOLog.Log();
        Move();
        var flags = controller.Move(direction);
        GOLog.Log(true, "controller.Move flags: " + flags);
    }

    private void LateUpdate()
    {
        GOLog.Log();
    }

    public Transform getLedgeChecker() {  return ledgeChecker;  }
    public Transform getGroundChecker() { return groundCheck; }
    public LayerMask getLedgeLayer() { return ledgeLayer; }
    public void ResetTargetLedge() { targetLedge = Vector3.negativeInfinity;  }
    public void TurnRight() { model.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0)); }
    public void TurnLeft() { model.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 0)); }

    private void Move()
    {
        direction.x = 0;
        bool tempGroundCheck = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        falling = false;
        // if there is any emptiness, fall down
        if (gravityActive && (!tempGroundCheck && !controller.isGrounded))
        {
            direction.y = gravity * Time.deltaTime;
            GOLog.Log(true, ">>>>> groundCheck.y: " + groundCheck.position.y);
            var nextGroundPosition = GroundFinder.NextPlatformBeneath(groundCheck.position);
            if (transform.position.y - nextGroundPosition.y > 7)
            {
                falling = true;
            }
            GOLog.Log(true, "groundCheck.y: " + groundCheck.position.y + " <<<<<<<");
        }
        else if (targetLedge.x != Vector3.negativeInfinity.x)  // moving toward the target ledge
        {
            // direction.x = (targetLedge.x - transform.position.x) * 0.1f; // Time.deltaTime;
            direction.x = (animator.rootRotation.y >= 0 ? 1 : -1) * moveSpeed * 0.05f;
            var currentDiff = targetLedge.x - transform.position.x;
            var nextDiff = currentDiff - direction.x;
            if ( Mathf.Abs(currentDiff) < 0.01f || Mathf.Abs(currentDiff) <= Mathf.Abs(nextDiff))
            {
                direction.x = 0;
            }
        }
        else if (state != null && state.canMoveWithoutInput)
        {
            direction.x = (animator.rootRotation.y >= 0 ? 1 : -1) * moveSpeed * Time.deltaTime;
        }
        else // character can move freely on ground
        {
            float deltaX = Input.GetAxisRaw("Horizontal");
            direction.x = deltaX * moveSpeed * Time.deltaTime;


            // turn character
            if (state != null && state.canChangeDirection && deltaX != 0)
            {
                model.rotation = Quaternion.LookRotation(new Vector3(deltaX, 0, 0));
            }
        }

        if (gravityActive == false || Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer))
        {
            direction.y = 0;
        }
    }

    private void logPosition(string s)
    {
        Debug.Log("Protagonist [transform position: " + transform.position + "]");
    }

    public void Climb()
    {
        if(climbDirection != 0)
        {
            controller.enabled = false;
            transform.position += new Vector3(0, 3 * climbDirection, 0);
            controller.enabled = true;
            climbDirection = 0;
        }
    }
}

