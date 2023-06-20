using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour  {
    [Header("Dash Ability")]
    [SerializeField] private float dashSpeed = 5f;
    [SerializeField] private float dashDuration = 1f;
    private bool isDashing = false;
    private float dashEndTime;
    private PlayerController playerController;
    public Animator animator;

    private void Awake() {
        playerController = GetComponent<PlayerController>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F)&& !isDashing) {
            playerController.animator.SetBool("isDashing",true);
            StartDash();
        }
        

        if (isDashing && Time.time >= dashEndTime) {
            playerController.animator.SetBool("isDashing", false);
            EndDash();
        }
    }

    private void StartDash() {
        isDashing = true;
        dashEndTime = Time.time + dashDuration;
        Vector3 dashDirection = playerController.moveDirection.normalized;
        playerController.rb.AddForce(dashDirection * dashSpeed, ForceMode.Impulse);
    }

    private void EndDash() {
        isDashing = false;
    }
}
