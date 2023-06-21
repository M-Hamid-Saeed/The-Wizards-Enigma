using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAI : MonoBehaviour
{
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Vector3.Slerp(transform.forward, roamPosition, 30 * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, roamPosition, 2 * Time.deltaTime);
        if (Vector3.Distance(transform.position, roamPosition) < 0.1f) {
            // Get a new random position to roam
            roamPosition = GetRandomPos();
        }
    }
    private Vector3 GetRandomDirection() {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f,-1f),Random.Range(-1f, -1f)).normalized;
    }
    private Vector3 GetRandomPos() {
        return startingPosition + GetRandomDirection() * Random.Range(10f, 70f);
    }
}
