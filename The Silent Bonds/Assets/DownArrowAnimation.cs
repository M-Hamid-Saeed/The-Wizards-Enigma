using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownArrowAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayAnimation();
    }


    void PlayAnimation()
    {
        float targetY = transform.localPosition.y  - 20;

        LeanTween.moveLocalY(gameObject, targetY, 0.5f).setEaseOutBack().setLoopPingPong();
    }

   
}
