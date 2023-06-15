using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator playerAnim;
    public PlayerController playercontrollerScript;
    
    void Update() {

       // WalkAnim();
    }
    private void WalkAnim() {
        /*Debug.Log(playercontrollerScript.vertical);
        if (playercontrollerScript.horizontal > 0.1f || playercontrollerScript.vertical > 0.1f) {
            playerAnim.SetBool("isWalking", true);
        }
        else 
            playerAnim.SetBool("isWalking", false);*/
    }
}

