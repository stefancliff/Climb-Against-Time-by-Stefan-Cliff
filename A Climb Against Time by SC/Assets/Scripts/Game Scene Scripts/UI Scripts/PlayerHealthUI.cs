using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public PlayerDeath player;
    

    public void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null) 
        {
            player = playerObject.GetComponent<PlayerDeath>();
        }
        else 
        {
            Debug.LogError("Player object not found with the 'Player' tag");
        }
    }
    void Update()
    {
        if(player != null) 
        {
            healthText.text = "Health: " + player.CurrentHealth.ToString();
        }
        else 
        {
            healthText.text = "Health: N/A";
        }
    }
}
