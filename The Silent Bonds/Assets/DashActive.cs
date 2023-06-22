using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashActive : MonoBehaviour
{
    [SerializeField] private DashAbility playerDash;
    [SerializeField] private ParticleSystem dashParticle;
    [SerializeField] private ParticleSystem stoppingParticle;
    [SerializeField] private ParticleSystem PortalBlue;
    // Start is called before the first frame update
    void Start()
    {
        playerDash.enabled = false;
    }

    
    private void OnTriggerEnter(Collider other) {
        Debug.Log("In trigger");
            stoppingParticle.Stop();
            playerDash.enabled = true;
            dashParticle.Play();
        PortalBlue.Play();
            Destroy(gameObject);
        
    }
    
}
