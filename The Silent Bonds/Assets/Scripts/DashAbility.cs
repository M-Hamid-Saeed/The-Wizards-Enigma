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

    [Header("Dashing Particle Section")]
    private Transform dashParticlesContainer;
    private ParticleSystem[] dashParticles;

    private void Awake() {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        // find the dash particle container and intialize the dash particle array with its childrens
        dashParticlesContainer = transform.Find("Particles").Find("DashParticles");
        Debug.Log(dashParticlesContainer.name + dashParticlesContainer.childCount);
        dashParticles = dashParticlesContainer.GetComponentsInChildren<ParticleSystem>();
        Debug.Log($"Component length = {dashParticles.Length}");
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
        PlayDashParticles();
    }

    private void EndDash() {
        isDashing = false;
    }

    private void PlayDashParticles()
    {
        dashParticlesContainer.forward = transform.Find("Player Visual").transform.forward;
        foreach (var particles in dashParticles)
            particles.Play();
    }
}
