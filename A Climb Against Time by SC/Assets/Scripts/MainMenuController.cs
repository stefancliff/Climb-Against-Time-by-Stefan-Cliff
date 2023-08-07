using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void OpenSettings()
    {
        // TODO: Implement the Settings pop-up window functionality
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
