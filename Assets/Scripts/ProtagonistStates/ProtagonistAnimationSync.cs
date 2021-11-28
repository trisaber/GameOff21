using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistAnimationSync : MonoBehaviour
{
    public Protagonist player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() { }

    public void AnimationStart(int animationId)
    {
        //Debug.Log("ProtagonistAnimationSync.AnimationStart: " + (Protagonist.ProtagonistActions)animationId);
        player.state.StartOfAnimation();
    }

    public void AnimationEnd(int animationId)
    {
        // Debug.Log(" ProtagonistAnimationSync.AnimationEnd: " + (ProtagonistStateBase.ProtagonistStates)animationId);
        player.state.EndOfAnimation();
    }

    public void AnimationActionStart(int animationId)
    {
        // GOLog.Log();
        player.state.StartOfAction();
    }

    public void AnimationActionEnd(int animationId)
    {
        // GOLog.Log();
        player.state.EndOfAction();
    }
}
