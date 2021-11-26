using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistAnimationSync : MonoBehaviour
{
    public string hey;
    public Protagonist player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() { }

    public void AnimationStart(int animationId)
    {
        //Debug.Log("ProtagonistAnimationSync.AnimationStart: " + (Protagonist.ProtagonistActions)animationId);
        //player.state.StartOfAnimation((Protagonist.ProtagonistActions)animationId);
    }

    public void AnimationEnd(int animationId)
    {
        Debug.Log(" ProtagonistAnimationSync.AnimationEnd: " + (ProtagonistStateBase.ProtagonistStates)animationId);
        player.state.EndOfAnimation();
    }
}
