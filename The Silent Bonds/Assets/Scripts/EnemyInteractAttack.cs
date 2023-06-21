using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInteractAttack : MonoBehaviour {
    [SerializeField] private float attackRadius = 3f;
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private Animator playerAnim;
    private NavmeshAgentBear bearnavMesh;
    private Animator anim;
    private EnemyHealthCombat playerHealth;
    private bool canAttack = true;
    private float attackDelay = 2f;
    [SerializeField]private float nextAttackTime = 2f;

    private void Start() {
        anim = GetComponent<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player Logic").GetComponent<EnemyHealthCombat>();
        bearnavMesh = GetComponent<NavmeshAgentBear>();
    }

    private void Update() {
        float distance = Vector3.Distance(targetPlayer.position, transform.position);
        if (distance <= attackRadius && Time.time >= nextAttackTime) {
            StopChasing();
            Attack();

            nextAttackTime = Time.time + attackDelay;
           
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    private void Attack() {
        
        anim.SetTrigger("Attack1");
        playerHealth.TakeDamage(attackDamage);
        canAttack = false;
    }

    private void StopChasing() {
        if (bearnavMesh != null) {
            bearnavMesh.stopChasing();
        }
    }

    private void ResumeChasing() {
        if (bearnavMesh != null) {
            bearnavMesh.startChasing();
        }
    }
}
