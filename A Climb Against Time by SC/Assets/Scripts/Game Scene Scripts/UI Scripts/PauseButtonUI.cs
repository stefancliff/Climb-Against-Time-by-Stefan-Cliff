using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseButtonUI : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject playerHealthUI;
    public PlayerMovement playerCharacter;

    public bool isPaused = false;
    
    private void Start()
    {
        
        GameObject playerObject = GameObject.FindWithTag("Player");

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
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        playerHealthUI.SetActive(true);
        isPaused = false;
        
        
    }
    public void PauseGame() 
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        playerHealthUI.SetActive(false);
        isPaused = true;
        
    }

    public void TogglePause()
    {
        if (isPaused)
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

