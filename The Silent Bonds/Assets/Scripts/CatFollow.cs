using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFollow : MonoBehaviour {
    public List<Transform> waypoints = new List<Transform>();
    public GameObject waypointParent;
    [SerializeField] private float speed;
    [SerializeField] private float lookRadius;
    public Transform targetPlayer;
    private Animator catAnim;
    private int index = 0;
    private float elapsedTime = 0f;
    private float miauTimer = 0f;
    private float miauIntervalMin = 7f;
    private float miauIntervalMax = 15f;
    public AudioClip meowSound;
    private AudioSource audioSource;
    private Rigidbody rb;

    private void Awake() {
        AssignWayPoints();
        catAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        float distance = Vector3.Distance(targetPlayer.position, transform.position);
        makeSound();
        if (distance <= lookRadius)
            startFollowWaypoints();
        else {
            catAnim.SetBool("isWalking", false);
            catAnim.SetTrigger("sit");
        }
    }

    private void startFollowWaypoints() {
        Vector3 currentDestination = waypoints[index].transform.position;

        rb.velocity = (currentDestination - transform.position).normalized * speed;

        Vector3 targetDirection = currentDestination - transform.position;
        if (targetDirection != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 30);
        }

        catAnim.SetBool("isWalking", true);

        if (Vector3.Distance(transform.position, currentDestination) <= 0.3f) {
            if (index < waypoints.Count - 1)
                index++;
            else {
                catAnim.SetTrigger("sit");
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                catAnim.SetBool("isWalking", false); // Stop the walking animation
            }
        }
    }

    private void AssignWayPoints() {
        for (int i = 0; i < waypointParent.transform.childCount; i++) {
            Transform child = waypointParent.transform.GetChild(i);
            waypoints.Add(child);
        }
    }

    private void makeSound() {
        elapsedTime += Time.deltaTime;
        miauTimer += Time.deltaTime;

        if (miauTimer >= Random.Range(miauIntervalMin, miauIntervalMax)) {
            audioSource.PlayOneShot(meowSound, 1.0f);
            miauTimer = 0f;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
