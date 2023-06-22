using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
    
}
