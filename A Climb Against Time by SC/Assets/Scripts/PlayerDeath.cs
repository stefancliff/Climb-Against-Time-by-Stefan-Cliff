using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    
    private Animator    PlayerAnimator;
    private Rigidbody2D PlayerCharacter;
    private SpriteRenderer PlayerAvatar;
    private int CurrentHealth;
    private bool takeHit = false;
    public int maxHealth = 3;
    //public float regenerationDelay = 10f; // seconds till health regeneration begins
    
    private void Start()
    {
        PlayerAnimator  = GetComponent<Animator>();
        PlayerCharacter = GetComponent<Rigidbody2D>();
        CurrentHealth   = maxHealth;
        takeHit = false;
        PlayerAnimator.SetBool("takeHit", takeHit);
    }

    void Update()
    {
        if (takeHit == true)
        {
            StartCoroutine(FadeTo(0.0f, 1.0f));
            StartCoroutine(FadeTo(1.0f, 1.0f));
            StartCoroutine(FadeTo(0.0f, 1.0f));
            StartCoroutine(FadeTo(1.0f, 1.0f));
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            TakeDamage(1);
        }
    }
    
    
    private void TakeDamage(int damage)
    {
        takeHit = true;
        PlayerAnimator.SetBool("takeHit", takeHit);
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, 3);

        Debug.Log("| " + damage + " <- Damage Taken | Health Remaining -> " + CurrentHealth + " |");

        if(CurrentHealth <= 0)
        {
            PlayerAnimator.SetTrigger("killPlayer");
            PlayerCharacter.bodyType = RigidbodyType2D.Static;
            RestartLevel();
        }

        takeHit = false;
        PlayerAnimator.SetBool("takeHit", takeHit);
    }

    IEnumerator FadeTo(float aValue, float aTime) 
    { 
        float alpha = transform.GetComponent<SpriteRenderer>().material.color.a; 
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) 
        { 
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t)); 
            transform.GetComponent<SpriteRenderer>().material.color = newColor; 
            yield return null; 
        } 
    } 

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
