using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Play Game Pressed");
        SceneManager.LoadScene("Game Scene");
    }

    public void OpenSettings()
    {
        // TODO: Implement the Settings pop-up window functionality
        Debug.Log("Settings Pressed");
    }

    public void ShowCredits()
    {
        Debug.Log("Credits Pressed");
        SceneManager.LoadScene("Credits Scene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game Pressed");
        Application.Quit();
        
    }
}
