using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGreenLevel2 : MonoBehaviour
{
    [SerializeField] private EnemyHealthCombat eagleHealth;
    [SerializeField] private EnemyHealthCombat PlayerHealth;
    [SerializeField] private ParticleSystem portalGreen;
    [SerializeField] private GameObject portal;
    [SerializeField] private LV2_UIManager level2Manager;

    bool animationPlayed = false;

    // Start is called before the first frame update
    private void Start() {
        portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (eagleHealth.isDead) {
            portal.SetActive(true);
            portalGreen.Play();

            if(!animationPlayed)
            {
                animationPlayed = true;
                level2Manager.OnLevelCompletion();
            }
                     
        }
        else if (PlayerHealth.isDead) {
            StartCoroutine(waitFortime());
            level2Manager.OnLevelFailed();
        }
        
    }
    IEnumerator waitFortime() {
        yield return new WaitForSeconds(3f);
    }

}
