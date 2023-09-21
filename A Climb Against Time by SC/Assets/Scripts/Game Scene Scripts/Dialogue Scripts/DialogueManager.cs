using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.IMGUI.Controls;
using System;
using Unity.VisualScripting.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public GameSession gameSession; 
    private Queue<string> sentences;
    public TMP_Text dialogueText;
    public TMP_Text speakerText;
    public GameObject dialogueUI;

    private int currentAttempt;
    
    private bool isTyping;
    

    private void Start()
    {
        sentences = new Queue<string>();
        dialogueUI.SetActive(false);
        currentAttempt = GameSession.instance.GetCurrentAttempt();
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
        
        //Debug.Log("Typing sentence: " + sentence);

        foreach (char c in sentence.ToCharArray())
        {
            dialogueText.text += c;
            
            yield return null;
        }

        isTyping = false;
        //Debug.Log("Typing complete: " + sentence);
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
}
