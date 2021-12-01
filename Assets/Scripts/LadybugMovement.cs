using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadybugMovement : MonoBehaviour
{
    private Collectable collectable = null;
    public Animator animator;

    private void Awake()
    {
        collectable = GetComponentInParent<Collectable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collectable.picked)
        {
            animator.SetBool("ready", true);
        }
    }
}
