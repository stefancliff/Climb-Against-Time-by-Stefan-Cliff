using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public float levelTimer = 90f; // 1.5 min
    public float currentTime;
    private bool isTimerPaused = true;
    public UnityEngine.UI.Image timerBar;

    void Start()
    {
        currentTime =  levelTimer;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTimerPaused)
        {
            // Per second reduction
            currentTime -= Time.deltaTime;

            // Clamping the timer to avoid negative values
            currentTime = Mathf.Clamp(currentTime, 0f, levelTimer);

            float fillAmount = currentTime / levelTimer;
            timerBar.fillAmount = fillAmount;

            if (currentTime <= 0f)
            {
                SceneManager.LoadScene("Game Scene");
                GameSession.instance.NextAttempt();
            }
        }
        
    }

    public void StopTimer()
    {
        isTimerPaused = true;
        
    }
    public void StartTimer()
    {
        isTimerPaused = false;
        
    }
}
