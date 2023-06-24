using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LV2_TimerScript : MonoBehaviour
{
    [SerializeField] private GameObject timerContainer;
    [SerializeField] private TextMeshProUGUI timerDisplayText;

    // variables
    public float timer = 20;
    private bool shouldUpdateTimer = true;

    bool levelFailed = false;


    // event
    public event EventHandler onTimerZero;


    private void Start()
    {
        //StartCoroutine(ResetTimerAfterDelay(7));
    }

    // Update is called once per frame
    void Update()
    {

        if (timer < 1 && !levelFailed)
        {
            Debug.Log("Timer reached zero!");
            onTimerZero?.Invoke(this, EventArgs.Empty);
            levelFailed = true;
            shouldUpdateTimer = false;
            return;
        }
            

        if (!shouldUpdateTimer) return;

        timer -= Time.deltaTime;
        updateTimerUI();

    }

    void updateTimerUI()
    {
        timerDisplayText.text =  returnFormatedTime();
    }

    public void StartTimer()
    {
        timerContainer.SetActive(true);
        shouldUpdateTimer = true;
    }

    public void PauseTimer()
    {
        timerContainer.SetActive(false);
        shouldUpdateTimer = false;
    }

    public void ResetTimer()
    {
        timer = 0.0f;
        shouldUpdateTimer = true;
        updateTimerUI();
    }

    public string returnFormatedTime()
    {
        int seconds = Mathf.FloorToInt(timer % 60);
        int minutes = Mathf.FloorToInt(timer / 60);

        return string.Format("{00:00}:{1:00}", minutes, seconds);
    }


    //IEnumerator ResetTimerAfterDelay(float seconds)
    //{
    //    yield return new WaitForSeconds(seconds);

    //    timer = 0.0f;
    //}
}
