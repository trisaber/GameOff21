using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistAnimationSync : MonoBehaviour
{
    public Protagonist player;
    void Start()
    {
        
    }
    public void AnimationEnd(int animationId)
    {
        Debug.Log("ProtagonistAnimationSync.AnimationEnd: " + (Protagonist.ProtagonistActions)animationId);
        // player.Climbing();
        player.state.EndOfAnimation();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
