using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.IMGUI.Controls;
using System;
using Unity.VisualScripting.InputSystem;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public GameSession gameSession; 
    private Queue<string> sentences;
    [Space]
    [Header("Dialogue UI ")]
    public TMP_Text dialogueText;
    public TMP_Text speakerText;
    public Image avatarImage;
    public GameObject dialogueUI;
    private int currentAttempt;
    private bool isTyping;
    
    [Space]
    [Header("Dialogue Triggers")]
    public GameObject[] dialogueTriggers;
    
    private void Awake()
    {
        currentAttempt = GameSession.instance.GetCurrentAttempt();
        UpdateDialogueTriggers(currentAttempt);
    }
    private void Start()
    {
        sentences = new Queue<string>();
        dialogueUI.SetActive(false);
        
    }
    
    private void Update()
    {
        SkipDialog();
    }

    public void StartConversation(Dialogue dialogue) 
    {
        Time.timeScale                  = 0f;
        GameSession.instance.isPaused   = true;
        speakerText.text                = dialogue.speaker;
        
        dialogueUI.SetActive(true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            //Debug.Log("Added sentence: " + sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeText(sentence));
    }

    IEnumerator TypeText(string sentence)
    {
        isTyping          = true;
        dialogueText.text = "";
        
        foreach (char c in sentence.ToCharArray())
        {
            dialogueText.text += c;
            
            yield return null;
        }

        isTyping = false;
        
    }

    private void EndDialogue()
    {
        dialogueUI.SetActive(false);
        GameSession.instance.isPaused   = false;
        Time.timeScale                  = 1f;
    }

    void OnDestroy()
    {
        if(GameSession.instance.isPaused)
        {
            Time.timeScale = 1f;
        }
    }

    public void SkipDialog()
    {
        if(isTyping)
        {
            if(Input.GetButtonDown("Submit"))
            {
                isTyping = false;
            }
            else if (Input.GetButtonDown("Submit")) 
                {
                    DisplayNextSentence();
                }
            
        }
    }

    public void UpdateDialogueTriggers(int currentAttempt)
    {
        switch(currentAttempt)
        {
            case 1:
                EnableOrDisableDialogueTriggers(currentAttempt);
            break;
            case 2:
                EnableOrDisableDialogueTriggers(currentAttempt);
            break;
            case 3:
                EnableOrDisableDialogueTriggers(currentAttempt);
            break;
            case 4:
                EnableOrDisableDialogueTriggers(currentAttempt);
            break;
            case 5:
                EnableOrDisableDialogueTriggers(currentAttempt);
            break;
            case 6:
                EnableOrDisableDialogueTriggers(currentAttempt);
            break;
        }
    }

    public void EnableOrDisableDialogueTriggers(int attemptNumber)
    {   
        for (int i = 0; i < dialogueTriggers.Length; i++)
        {
            if (i == currentAttempt - 1) 
            {
                dialogueTriggers[i].SetActive(true);
            }
            else 
            {
                dialogueTriggers[i].SetActive(false);
            }
        }
    }
}
