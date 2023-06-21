using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshAgentBear : MonoBehaviour {
    private NavMeshAgent bear;
    [SerializeField] private Animator animator;
    [SerializeField] private float lookRadius;
    public Transform targetPlayer;
    private AudioSource audioSource;
    public AudioClip growlAudio;
    private bool hasPlayedAudio = false;
    private float startAnimTime;
    [SerializeField]private float animDuration = 2f;
    private bool hasStartedAnimation = false;

    // Start is called before the first frame update
    void Start() {
        bear = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(targetPlayer.position, transform.position);
        
        if (distance <= lookRadius) {
            animator.SetTrigger("Buff");
            if (!hasStartedAnimation) {
                startAnimTime = Time.time;
                hasStartedAnimation = true;
            }

            if (Time.time <= startAnimTime + animDuration) {
                stopChasing();
            }
            else {
                startChasing();
            }

            if (!hasPlayedAudio) {
                audioSource.PlayOneShot(growlAudio, 1f);
                hasPlayedAudio = true;
            }

            if (distance <= bear.stoppingDistance) {
                FaceTarget();
            }
        }
        else {
            hasPlayedAudio = false; // Reset the flag when the enemy is out of range
            hasStartedAnimation = false; // Reset the flag when the enemy is out of range
            stopChasing(); // Stop chasing and reset velocity and angularVelocity
        }
    }

    private void FaceTarget() {
        Vector3 direction = (targetPlayer.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    public void stopChasing() {
        bear.SetDestination(transform.position);
        bear.velocity = Vector3.zero;
        
    }

    public void startChasing() {
        bear.SetDestination(targetPlayer.position);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
