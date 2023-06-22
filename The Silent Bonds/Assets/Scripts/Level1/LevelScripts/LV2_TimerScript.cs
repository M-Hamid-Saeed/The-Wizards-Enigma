using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LV2_TimerScript : MonoBehaviour
{
    [SerializeField] private GameObject timerContainer;
    [SerializeField] private TextMeshProUGUI timerDisplayText;

    // variables
    public float timer = 0.0f;
    private bool shouldUpdateTimer = true;



    // Update is called once per frame
    void Update()
    {
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
}
