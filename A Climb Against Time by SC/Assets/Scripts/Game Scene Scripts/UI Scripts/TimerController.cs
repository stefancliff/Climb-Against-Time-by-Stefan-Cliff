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
    private bool removeTimer = false;

    void Start()
    {
        currentTime =  levelTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(removeTimer == false)
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
        else
        {
            currentTime = levelTimer;
            timerBar.enabled = false;
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

    public void EndGameTimer()
    {
        removeTimer = true;
    }
}
