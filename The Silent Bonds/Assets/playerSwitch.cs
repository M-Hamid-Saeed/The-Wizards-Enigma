using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSwitch : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject eagle;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject eagleCamera;
    [SerializeField] private EagleController eagleController;
    [SerializeField] private EagleFollowPlayer eagleFollowPlayerScript;
    [SerializeField] private PlayerController playerController;

    private bool isPlayerActive = true;

    void Start() {
        // Activate player and its corresponding components
        player.SetActive(true);
        playerCamera.SetActive(true);
        playerController.enabled = true;

        // Deactivate eagle and its corresponding components
        eagleCamera.SetActive(false);
        eagleController.enabled = false;
        eagleFollowPlayerScript.enabled = true;
    }

    void Update() {
        // Toggle between player and eagle on "C" key press
        if (Input.GetKeyDown(KeyCode.C)) {
            isPlayerActive = !isPlayerActive;

            if (isPlayerActive) {
                // Activate player and its corresponding components
                playerCamera.SetActive(true);
                playerController.enabled = true;

                // Deactivate eagle and its corresponding components
                eagleCamera.SetActive(false);
                eagleController.enabled = false;
                eagleFollowPlayerScript.enabled = true;
            }
            else {
                // Deactivate player and its corresponding components
                playerCamera.SetActive(false);
                playerController.enabled = false;

                // Activate eagle and its corresponding components
                eagleCamera.SetActive(true);
                eagleController.enabled = true;
                eagleFollowPlayerScript.enabled = false;
            }
        }
    }
}
