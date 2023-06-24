using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Device;

public class DashActive : MonoBehaviour
{
    [SerializeField] private DashAbility playerDash;
    [SerializeField] private ParticleSystem dashParticle;
    [SerializeField] private ParticleSystem stoppingParticle;
    [SerializeField] private ParticleSystem PortalBlue;
    [SerializeField] private GameObject portalGameObject;

    [SerializeField] private LV3_UIManager uiManager;
    [SerializeField] private GameObject miniObjectiveContainer;
    [SerializeField] private TextMeshProUGUI dashAcquired;


    float animationTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        playerDash.enabled = false;
        portalGameObject.SetActive(false);
    }

    
    private void OnTriggerEnter(Collider other) {
        Debug.Log("In trigger");
            stoppingParticle.Stop();
            playerDash.enabled = true;
            dashParticle.Play();
        PortalBlue.Play();
        portalGameObject.SetActive(true);
            Destroy(gameObject);
        uiManager.DisplayMiniObjective(false);
        DisplayPortalPrompt(true);
        dashAcquired.gameObject.SetActive(true);
        StartCoroutine(HideDashInfo());
    }

    void DisplayPortalPrompt(bool onScreen)
    {
        float offScreenX = -1309;
        float targetX = -628;

        if (!onScreen) targetX = offScreenX;

        Debug.Log("Display Mini Objective");
        LeanTween.moveLocalX(miniObjectiveContainer, targetX, animationTime / 2).setEaseOutQuart();
    }

    IEnumerator HideDashInfo()
    {
        yield return new WaitForSeconds(3);
        dashAcquired.gameObject.SetActive(false);
    }
    
}
