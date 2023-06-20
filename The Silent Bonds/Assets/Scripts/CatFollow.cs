using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFollow : MonoBehaviour {
    public List<Transform> waypoints = new List<Transform>();
    public GameObject waypointParent;
    [SerializeField] private float speed;
    private Animator catAnim;
    private int index = 0;
    private float elapsedTime = 0f;
    private float miauTimer = 0f;
    private float miauIntervalMin = 7f;
    private float miauIntervalMax = 15f;
    public AudioClip meowSound;
    private AudioSource audio;
    private Rigidbody rb;
    private void Awake() {
        AssignWayPoints();
        catAnim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        elapsedTime += Time.deltaTime;
        miauTimer += Time.deltaTime;

        if (miauTimer >= Random.Range(miauIntervalMin, miauIntervalMax)) {
           
            audio.PlayOneShot(meowSound, 1.0f);
            miauTimer = 0f;
        }

        Vector3 currentDestination = waypoints[index].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, currentDestination, speed * Time.deltaTime);

        Vector3 targetDirection = currentDestination - transform.position;
        if (targetDirection != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 50);
        }

        catAnim.SetBool("isWalking", true);

        if (Vector3.Distance(transform.position, currentDestination) <= 0.08f) {

            if (index < waypoints.Count - 1)
                index++;
            else {
                catAnim.SetTrigger("sit");
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }
        
    }

    private void AssignWayPoints() {
        for (int i = 0; i < waypointParent.transform.childCount; i++) {
            Transform child = waypointParent.transform.GetChild(i);
            waypoints.Add(child);
        }
    }
}
