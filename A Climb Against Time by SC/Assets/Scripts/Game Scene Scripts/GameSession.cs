using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession instance;
    public DialogueManager dialogueManager;
    public bool isPaused        = false;
    public int currentAttempt  = 1;

    private void Awake()
    {
        currentAttempt = 1;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if(currentAttempt == 0)
        {
            currentAttempt = 1;
        }

        dialogueManager = GetComponent<DialogueManager>();
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
