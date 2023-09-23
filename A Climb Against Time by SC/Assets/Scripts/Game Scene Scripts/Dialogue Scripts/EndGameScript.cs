using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public Dialogue dialogue;
    public TimerController timerController;
    private CameraScript cameraScript;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        
        if(playerObject != null)
        {
            Transform cameraTransform = playerObject.transform.Find("Camera(Clone)");

            if(cameraTransform != null)
            {
                cameraScript = cameraTransform.GetComponent<CameraScript>();
            }
            else
            {
            Debug.LogError("Camera GameObject not found");
            }
        }
        else
        {
            Debug.LogError("Player Object not found");
        }
        
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();
            dialogueManager.StartConversation(dialogue);
            timerController.EndGameTimer();
            if(cameraScript != null)
            {
                cameraScript.isAtEndOfLevel = true;
            }
        }
        StartCoroutine(LoadCreditsSceneAfterDelay());
    }

    IEnumerator LoadCreditsSceneAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Credits Scene");
    }
}
