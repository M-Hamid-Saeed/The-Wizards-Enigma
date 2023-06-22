using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGreenLevel2 : MonoBehaviour
{
    [SerializeField] private EnemyHealthCombat eagleHealth;
    [SerializeField] private ParticleSystem portalGreen;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (eagleHealth.isDead) {
            portalGreen.Play();
        }
        
    }
}
