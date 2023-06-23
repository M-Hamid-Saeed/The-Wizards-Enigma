using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV3_UIManager : MonoBehaviour
{
    [SerializeField] private GameObject introTitle;
    [SerializeField] private GameObject objectiveContainer;

    [SerializeField] private GameObject miniObjectiveContainer;
    [SerializeField] private GameObject checkMarkMiniObjective;

    [SerializeField] private GameObject levelCompletedContainer;
    [SerializeField] private GameObject levelFailedContainer;

    float animationTime = 3f;


    [SerializeField] private AudioManager_LV2 audioManager;
    private bool animationsFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayIntroAnimation();
        audioManager.Play("Suspense");
    }

    // Update is called once per frame
    void Update()
    {

    }


    void PlayIntroAnimation()
    {
        PlayTitleAnimation(true);

        StartCoroutine(CloseTitleOpenObjective(animationTime));
        // StartCoroutine(Test(animationTime * 5));
    }

    void PlayTitleAnimation(bool onScreen)
    {
        float offScreenY = 700;
        float targetY = 0;

        if (!onScreen)
            targetY = offScreenY;


        LeanTween.moveLocalY(introTitle, targetY, animationTime).setEaseOutQuart();
    }


    public void PlayObjectiveAnimation(bool onScreen)
    {
        float offScreenY = -1000;
        float targetY = 0;
        float time = animationTime;

        if (!onScreen)
        {
            targetY = offScreenY;
            DisplayMiniObjective(true);
            time /= 2;
            animationsFinished = true;
        }
        else
        {
            DisplayMiniObjective(false);
            audioManager.Play("NotificationSound");
        }



        LeanTween.moveLocalY(objectiveContainer, targetY, time).setEaseOutQuart();

        // play audio here
    }

    public void DisplayMiniObjective(bool onScreen)
    {
        float offScreenX = -1309;
        float targetX = -628;

        if (!onScreen) targetX = offScreenX;

        Debug.Log("Display Mini Objective");
        LeanTween.moveLocalX(miniObjectiveContainer, targetX, animationTime / 2).setEaseOutQuart();

    }

    public void OnLevelCompletion()
    {
        // audioManager.Play("LevelCompleted");
        LeanTween.scale(checkMarkMiniObjective, new Vector3(1, 1, 1), 0.25f).setEaseOutBack();
        audioManager.Play("Yoo");
        audioManager.Play("Stamp");
        levelCompletedContainer.SetActive(true);
        LeanTween.scale(levelCompletedContainer, new Vector3(1, 1, 1), 0.5f).setEaseOutElastic();
    }

    public void OnLevelFailed()
    {
        audioManager.Play("Failed");
        levelCompletedContainer.SetActive(true);
        LeanTween.scale(levelFailedContainer, new Vector3(1, 1, 1), 0.5f).setEaseOutElastic();
    }

    IEnumerator CloseTitleOpenObjective(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        PlayTitleAnimation(false);
        PlayObjectiveAnimation(true);
    }

    public bool AreAnimationsFinished() => animationsFinished;
}
