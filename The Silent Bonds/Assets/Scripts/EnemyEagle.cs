using UnityEngine;

public class EnemyEagle : MonoBehaviour {
    public float attackDamage = 10f;
    public float attackCooldown = 2f;
    public float attackRange = 2f; // Distance to maintain from the player
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator eagleAnimator;
    [SerializeField] private GameObject eagleVisual;
    private float minAttackDelay = 2f;
    private float maxAttackDelay = 6f;
    private float attackDelay;
    private bool isAttacking = false;
    private bool isFacingPlayer = false;
    private bool isPlayerInRange = false;
    private float attackStartTime;

    private void Start() {
        // playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // eagleAnimator = GetComponent<Animator>();
    }

    private void Update() {
        if (isPlayerInRange && !isAttacking) {
            // Start attacking the player
            attackDelay = Random.Range(minAttackDelay, maxAttackDelay);
            attackStartTime = Time.time;
            isAttacking = true;
        }

        if (isAttacking) {
            MoveAndRotateTowardsPlayer();
            if (Time.time - attackStartTime >= attackDelay) {
                AttackPlayer();
                isAttacking = false;
            }
        }
    }

    public void PlayerEnteredRange() {
        isPlayerInRange = true;
    }

    public void PlayerExitedRange() {
        isPlayerInRange = false;
    }

    private void AttackPlayer() {
        // Rotate the eagle towards the player
        Vector3 targetDirection = playerTransform.position - eagleVisual.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        eagleVisual.transform.rotation = Quaternion.Slerp(eagleVisual.transform.rotation, targetRotation, Time.deltaTime * 20f);

        isFacingPlayer = true;

        // Attack the player (perform any attack animation or logic here)
        eagleAnimator.SetTrigger("Attack");
    }

    private void MoveAndRotateTowardsPlayer() {
        if (!isFacingPlayer) {
            // Rotate the eagle towards the player
            Vector3 targetDirection = playerTransform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20f);
        }

        // Calculate the direction to move away from the player
        Vector3 moveDirection = transform.position - playerTransform.position;
        moveDirection.Normalize();

        // Calculate the target position to maintain distance from the player
        Vector3 targetPosition = playerTransform.position + moveDirection * attackRange;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2 * Time.deltaTime);
    }
}
