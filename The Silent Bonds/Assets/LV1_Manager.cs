using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;

public class LV1_Manager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCamera;
    [SerializeField] private GameObject thirdPersonCamera;

    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject playerHUD;

    [SerializeField] private GameObject miniObjectiveContainer;

    float animationTime = 2.0f;
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

            PlayMiniObjectiveAnimation(true);
        }
    }

    void PlayMiniObjectiveAnimation(bool onScreen)
    {
        float offScreenX = -1309;
        float targetX = -628;

        if (!onScreen) targetX = offScreenX;

        Debug.Log("Display Mini Objective");
        LeanTween.moveLocalX(miniObjectiveContainer, targetX, animationTime / 2).setEaseOutQuart();
    }
}
