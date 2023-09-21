using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession instance;
    public bool isPaused        = false;
    private int currentAttempt  = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextAttempt()
    {
        currentAttempt++;
    }

    public int GetCurrentAttempt()
    {
        return currentAttempt;
    }
}
