using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{

    private Level5Manager levelManager;

    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Level5Manager>();
        if (levelManager) Debug.Log("Manager acquired!");
    }
    private void OnTriggerEnter(Collider other) {
        levelManager.CheckPointAcquired();
        Destroy(gameObject);
    }
    
}
