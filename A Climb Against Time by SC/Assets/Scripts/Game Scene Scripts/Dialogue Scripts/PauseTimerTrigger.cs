using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTimerTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public TimerController timerController;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();
            dialogueManager.StartConversation(dialogue);
            timerController.StopTimer();

            // Here I am deactivating the trigger so that it won't run again if the player enters
            gameObject.SetActive(false);
        }
    }
}
