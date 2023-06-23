using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LV1_Manager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCamera;
    [SerializeField] private GameObject thirdPersonCamera;

    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject playerHUD;

    bool enteredScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!enteredScene && Input.GetKeyDown(KeyCode.Space))
        {
            enteredScene = true;
            mainMenuCanvas.SetActive(false);
            mainMenuCamera.SetActive(false);
            thirdPersonCamera.SetActive(true);
            playerHUD.SetActive(true);
        }
    }
}
