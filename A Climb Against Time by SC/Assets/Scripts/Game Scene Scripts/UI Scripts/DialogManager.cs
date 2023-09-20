using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
   public TMP_Text dialogText;
   public TMP_Text speakerLabel;
   public Image avatarImage;

   [TextArea(3, 10)]
   public string inspectorText;
   public string currentText;
   private bool isTyping;
   private bool isPaused;

    public void StartDialog(string text, Sprite avatar, string speakerName)
    {
        currentText = text;
        avatarImage.sprite = avatar;
        speakerLabel.text = speakerName;
        StartCoroutine(TypeText());
        Time.timeScale = 0f;
        isPaused = true;
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        dialogText.text = "";

        foreach(char c in currentText)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }

    public void SkipOrCloseDialog()
    {
        if(isTyping)
        {
            if(Input.GetButtonDown("Submit"))
            {
                StopCoroutine(TypeText());
                dialogText.text = currentText;
                isTyping = false;
            }
            else
            {
                if(Input.GetButtonDown("Submit")) 
                {
                    dialogText.text = "";
                    avatarImage.sprite = null;
                    speakerLabel.text = "";
                    Time.timeScale = 1f;
                    isPaused = false;
                }
            }
        }
    }

    void OnDestroy()
    {
        if(isPaused)
        {
            Time.timeScale = 1f;
        }
    }
}
