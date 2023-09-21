using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseButtonUI : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject playerUI;
    public PlayerMovement playerCharacter;
    public GameSession gameSession;
    
    private void Start()
    {
        
        GameObject playerObject = GameObject.FindWithTag("Player");
        gameSession             = GetComponent<GameSession>();

        if (playerObject != null) 
        {
            playerCharacter = playerObject.GetComponent<PlayerMovement>();
            
        }
        else 
        {
            Debug.LogError("Player object not found with the 'Player' tag");
        }
        ResumeGame();

    }

    private void Update()
    {
        
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame) 
        {
            TogglePause();
            
        }
    }

    public void ResumeGame() 
    {
        GameSession.instance.isPaused   = false;
        Time.timeScale                  = 1f;
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        
        
    }
    public void PauseGame() 
    {
        GameSession.instance.isPaused   = true;
        Time.timeScale                  = 0f;
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(false);
        
    }

    public void TogglePause()
    {
        if (GameSession.instance.isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

