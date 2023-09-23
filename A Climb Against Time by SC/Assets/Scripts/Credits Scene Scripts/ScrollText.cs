using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScrollText : MonoBehaviour
{
    public float scrollSpeed = 100f;
    public TextMeshProUGUI creditsText;

    private RectTransform textRectTransform;
    private Vector2 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        creditsText         = GetComponent<TextMeshProUGUI>();
        textRectTransform   = creditsText.GetComponent<RectTransform>();
        initialPosition     = textRectTransform.anchoredPosition;

        StartCoroutine(CreditsRoll());
    }

    void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }


    private IEnumerator CreditsRoll()
    {
        float textHeight = creditsText.preferredHeight;
        float panelHeight = textRectTransform.rect.height;

        while (textRectTransform.anchoredPosition.y < textHeight - panelHeight)
        {
            Vector2 position = textRectTransform.anchoredPosition;
            position.y += scrollSpeed * Time.deltaTime;
            textRectTransform.anchoredPosition = position;
            yield return null;
        }

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Main Menu");
    }
}
