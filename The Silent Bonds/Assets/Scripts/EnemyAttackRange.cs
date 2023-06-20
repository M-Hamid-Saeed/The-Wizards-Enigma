using UnityEngine;

public class EnemyAttackRange : MonoBehaviour {
    public EnemyEagle enemy; // Reference to the enemy script

 
    private void OnTriggerEnter(Collider other) {
        Debug.Log("_________");
        if (other.gameObject.CompareTag("Player Logic")) {
            Debug.Log("Trigger Enter");
            enemy.PlayerEnteredRange();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player Logic")) {
            Debug.Log("Trigger Exit");
            enemy.PlayerExitedRange();
        }
    }
}
