using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGreenLevel2 : MonoBehaviour
{
    [SerializeField] private EnemyHealthCombat eagleHealth;
    [SerializeField] private ParticleSystem portalGreen;
    [SerializeField] private GameObject portal;
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
        }
        
    }
}
