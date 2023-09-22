using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public Dialogue dialogue;
    public TimerController timerController;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();
            dialogueManager.StartConversation(dialogue);
            this.timerController.StopTimer();
            CameraScript cameraScript = GetComponent<CameraScript>();
            cameraScript.isAtEndOfLevel = true;
            

            // Here I am deactivating the trigger so that it won't run again if the player enters
            gameObject.SetActive(false);
        }

        SceneManager.LoadScene("Main Menu");
    }
}
