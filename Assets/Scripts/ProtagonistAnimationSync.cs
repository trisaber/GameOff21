using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationSync : MonoBehaviour
{
    public Movement player;
    void Start()
    {
        
    }
    public void ClimbedUp()
    {
        // player.Climbing();
        player.state.EndOfAnimation();
    }

    public void JumpMissedGrab()
    {
        player.state.EndOfAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
