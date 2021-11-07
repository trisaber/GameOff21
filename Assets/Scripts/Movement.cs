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

    bool isHanging = false;
    bool isGrounded;
    public Animator animator;
    public Transform model;
    public float deltaX;





    #endregion

    void Start()
    {

    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        if (isGrounded&!isHanging)  //checks if the player is grounded
        {
            animator.SetBool("isJump", false);
             animator.SetBool("isGrounded", true);
             direction.y = 0;
             Move();
             Jump();
        }
        else if(isHanging)
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
        if(Input.GetButtonDown("Jump"))
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
        deltaX = Input.GetAxis("Horizontal") ;
        direction.x = deltaX * Movespeed * Time.deltaTime;

        animator.SetFloat("speed", Mathf.Abs(deltaX));// spped run animationı çalıştırıyor.
        if (deltaX!=0)
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
    public void Climbed()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        isHanging = false;
        animator.SetBool("isHanging", isHanging);
        animator.SetBool("isJump", false);
    }
}

