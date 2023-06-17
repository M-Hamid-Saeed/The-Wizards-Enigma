using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilties: PlayerController
{
    [Header("Dash Ability")]
    public  float dashSpeed = 5f;
    public float dashTimer = 1f;
    private float startDashingTime;

    private void Update() {
        dashAbility();
    }
    private void dashAbility() {
        if (Input.GetMouseButtonDown(1)) { 

            startDashingTime = Time.time;
            Vector3 direction = moveDirection;
            while(Time.time < startDashingTime+dashTimer)
                rb.AddForce(direction * dashSpeed, ForceMode.Impulse);

        }
    }
}
