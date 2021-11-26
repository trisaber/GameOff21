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
    public Collider jumpingCollider;
    #endregion

    // member variables
    public ProtagonistStateBase state = null;
    public int climbDirection = 0;

    private void Start()
    {
    }

    private void FixedUpdate() { }

    private void Update()
    {
        Move();
        controller.Move(direction);
    }

    private void LateUpdate() { }

    public Transform getLedgeChecker() {  return ledgeChecker;  }
    public LayerMask getLedgeLayer() { return ledgeLayer; }

    //public Collider CheckLedgeCollide2()
    //{
    //    //if(Physics.CheckSphere(context.ledgeChecker.position, 0.15f, context.ledgeLayer))
    //    Collider[] colliders = Physics.OverlapSphere(ledgeChecker.position, 0.15f * moveSpeed, ledgeLayer);
    //    foreach (Collider c in colliders)
    //    {
    //        if (model.rotation.y > 0 && c.name == "LeftLedge" && model.position.x <= (c.transform.position.x + 0.15f))
    //        {
    //            // Debug.Log("Right ledge grabbed. pos.x: " + c.transform.position.x + ", diff x: " + (c.transform.position.x - context.ledgeChecker.position.x));
    //            // Debug.Log("ledge checker.x: " + context.ledgeChecker.transform.position.x + ", pos.x: " + context.transform.position.x);
    //            return c;
    //        }
    //        if (model.rotation.y < 0 && c.name == "RightLedge" && model.position.x >= (c.transform.position.x - 0.15f))
    //        {
    //            // Debug.Log("Right ledge grabbed. pos.x: " + c.transform.position.x + ", diff x: " + (c.transform.position.x - context.ledgeChecker.position.x));
    //            // Debug.Log("ledge checker.x: " + context.ledgeChecker.transform.position.x + ", pos.x: " + context.transform.position.x);
    //            return c;
    //        }
    //    }

    //    return null;
    //}

    private void Move()
    {
        // if there is any emptiness, fall down
        if (!Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer))
        {
            direction.y += gravity * Time.deltaTime;
        }
        else // character can move freely on ground
        {
            float deltaX = Input.GetAxisRaw("Horizontal");
            direction.x = deltaX * moveSpeed * Time.deltaTime;


            // turn character
            if (state.canChangeDirection && deltaX != 0)
            {
                model.rotation = Quaternion.LookRotation(new Vector3(deltaX, 0, 0));
            }
        }
    }

    private void logPosition(string s)
    {
        Debug.Log(s + " [transform position: " + transform.position + "]");
    }

    public void Climb()
    {
        if(climbDirection != 0)
        {
            Debug.Log("Protagonist.Climb");
            controller.enabled = false;
            transform.position += new Vector3(0, 3 * climbDirection, 0);
            controller.enabled = true;
            climbDirection = 0;
        }
    }
}

