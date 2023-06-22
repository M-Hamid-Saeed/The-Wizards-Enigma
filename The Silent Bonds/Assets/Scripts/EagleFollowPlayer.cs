using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleFollowPlayer : MonoBehaviour {
    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed;
    [SerializeField] private float rotationSpeed;

    private float initialYPosition;

    private void Start() {
        initialYPosition = transform.position.y;
    }

    private void Update() {
        Vector3 targetPosition = new Vector3(player.position.x - 3f, initialYPosition, player.position.z - 3f);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
