using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugsMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    public Transform model;
    [SerializeField] CharacterController cc;
    [SerializeField] float speed = 4.0f;
    [SerializeField] BugsDirection moveDirection = BugsDirection.Right; 

    private bool action = false;
    private Vector3 direction;
    private Dictionary<BugsDirection, Vector3> moveDirections = new Dictionary<BugsDirection, Vector3>();
    private float moveStartTime = 0;

    enum BugsDirection
    {
        Idle = 0,
        Right = 1,
        Left = 2,
        ToTree = 3,
    }

    private void Awake()
    {
        moveDirections[BugsDirection.Idle] = new Vector3(0, 0, 0);
        moveDirections[BugsDirection.Right] = new Vector3(1, 0, 0);
        moveDirections[BugsDirection.Left] = new Vector3(-1, 0, 0);
        moveDirections[BugsDirection.ToTree] = new Vector3(0, 0, 1);
    }

    // Start is called before the first frame update
    void Start()
    {   
    }

    private void FixedUpdate()
    {
        if (Input.anyKey)
        {
            action = true;
            model.rotation = Quaternion.LookRotation(moveDirections[moveDirection]);
            animator.SetInteger("animationId", 4);

            moveStartTime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (action)
        {
            direction.x = moveDirections[moveDirection].x * speed * Time.deltaTime;
            direction.y = moveDirections[moveDirection].y * speed * Time.deltaTime;
            direction.z = moveDirections[moveDirection].z * speed * Time.deltaTime;
            var flags = cc.Move(direction);
            Debug.Log("bugs flags: " + flags);

            if (Time.time - moveStartTime > 3)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
