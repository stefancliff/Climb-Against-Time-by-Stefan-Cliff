using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();
            dialogueManager.StartConversation(dialogue);

            // Here I am deactivating the trigger so that it won't run again if the player enters
            gameObject.SetActive(false);
        }
    }
}
