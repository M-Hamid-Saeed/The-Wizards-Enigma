using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Manager : MonoBehaviour
{
    [SerializeField] private LV5_UIManager uiManager;
    [SerializeField] private GameObject levelPrompt;
    [SerializeField] private GameObject downArrow;
    [SerializeField] private GameObject playerHUD;

    bool playedOnce;
    int checkpoints = 0;
    float animationTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("PlayerHUD");
        if (playerHUD) Debug.Log("playerHUD acquired"); else Debug.Log("PlayerHUD not found");
    }

    // Update is called once per frame
    void Update()
    {
        if (checkpoints <= 1 || playedOnce)
            return;


        playedOnce = true;
        uiManager.DisplayMiniObjective(false);
        DisplayLevelPrompt(true);
        downArrow.SetActive(true);
        playerHUD.transform.Find("Levels").gameObject.SetActive(true) ;
    }

    public void CheckPointAcquired()
    {
        checkpoints++;
    }

    public void DisplayLevelPrompt(bool onScreen)
    {
        float offScreenX = -1309;
        float targetX = -628;

        if (!onScreen) targetX = offScreenX;

        Debug.Log("Display Mini Objective");
        LeanTween.moveLocalX(levelPrompt, targetX, animationTime / 2).setEaseOutQuart();
    }
}
