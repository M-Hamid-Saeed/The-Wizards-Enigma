using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LV2_Manager : MonoBehaviour
{
    [SerializeField] private MouseLockManager mouseLockManager;
    [SerializeField] private LV2_UIManager uiManager;
    [SerializeField] private LV2_TimerScript timerManager;
    // Start is called before the first frame update
    void Start()
    {
        mouseLockManager.LockMouse();
        timerManager.onTimerZero += TimerManager_onTimerZero;
    }

    private void TimerManager_onTimerZero(object sender, System.EventArgs e)
    {
        onLevelFailed();
    }

    // Update is called once per frame
    void Update()
    {
        if (uiManager.AreAnimationsFinished())
            timerManager.StartTimer();
    }


    public void OnLevelComplete()
    {
        timerManager.PauseTimer();
        uiManager.OnLevelCompletion();
    }

    public void onLevelFailed()
    {
        timerManager.PauseTimer();
        uiManager.OnLevelFailed();
    }
}