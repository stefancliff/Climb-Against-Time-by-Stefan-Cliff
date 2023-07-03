using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsWindow;
    public float scrollSpeed = 10f;
    public KeyCode skipKey = KeyCode.Escape;

    private bool scrollingCredits = false;
    private RectTransform creditsTextRectTransform;
    private float creditsTextHeight;

    private void Start()
    {
        creditsTextRectTransform = creditsWindow.GetComponentInChildren<RectTransform>();
        creditsTextHeight = creditsTextRectTransform.rect.height;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        creditsWindow.SetActive(true);
        scrollingCredits = true;
    }

    private void Update()
    {
        if (scrollingCredits)
        {
            float scrollAmount = scrollSpeed * Time.deltaTime;

            if (Input.GetKey(KeyCode.DownArrow))
            {
                scrollAmount *= 2f;
            }

            creditsTextRectTransform.anchoredPosition += new Vector2(0f, scrollAmount);

            if (Input.GetKeyDown(skipKey) || creditsTextRectTransform.anchoredPosition.y > creditsTextHeight)
            {
                CloseCredits();
            }
        }
    }

    private void CloseCredits()
    {
        creditsWindow.SetActive(false);
        scrollingCredits = false;
    }
}
