using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyHealthCombat : MonoBehaviour
{
    [Header("\tEnemy Health\n")]
    [SerializeField] private int health = 100;
    [SerializeField]private int currentHealth;
    [Header("\tAnimation Section\n")]
    [SerializeField] private Animator anim;
    public EnemyHealthUI healthUI;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        healthUI.setMaxHealth(health);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead && gameObject.CompareTag("Player Logic"))
            ReloadScene();
    }

    public void TakeDamage(int damage) {
        Debug.Log("TAKING DAMAGE");
        anim.SetTrigger("GetHit");
        currentHealth -= damage;
        healthUI.setHealth(currentHealth);
        if (currentHealth <= 0) {
            Die();
            isDead = true;
        }
    }
    private void Die() {
        anim.SetTrigger("Death");
        StartCoroutine(destroy());

    }
    IEnumerator destroy() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
