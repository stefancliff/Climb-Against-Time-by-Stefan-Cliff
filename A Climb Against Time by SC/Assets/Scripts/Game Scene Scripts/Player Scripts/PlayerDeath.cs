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
    public int CurrentHealth;
    public int maxHealth = 3;
    public bool takeHit = false;
    [SerializeField] Vector2 damageKick = new Vector2(1f, 8.5f);
    
    
    private void Start()
    {
        PlayerAnimator  = GetComponent<Animator>();
        PlayerCharacter = GetComponent<Rigidbody2D>();
        PlayerAvatar    = GetComponent<SpriteRenderer>();
        CurrentHealth   = maxHealth;
        takeHit         = false;
        PlayerAnimator.SetBool("takeHit", takeHit);
        
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
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        
        PlayerAvatar.enabled = false;
        PlayerAvatar.enabled = true;
        PlayerCharacter.velocity = damageKick;

        takeHit = false;
        PlayerAnimator.SetBool("takeHit", takeHit);
        PlayerAvatar.enabled = true;

        if (CurrentHealth <= 0)
        {
            PlayerAnimator.SetTrigger("killPlayer");
            PlayerCharacter.bodyType = RigidbodyType2D.Static;
            RestartLevel();
        }

        
    }

    

    private void RestartLevel()
    {
        GameSession.instance.NextAttempt();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /* -- The idea was to make the player sprite flash a few times as an extra way for the player to know if they took damage
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
    */

    /* An idea to, over the course of ten seconds, let the players health regenerate
    
    private float regenInterval = 10f; // seconds till health regeneration begins
    private float regenCooldown;

    private void RegenerateHealth()
    {
        CurrentHealth++;
        regenCooldown = Time.deltaTime + regenInterval;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
    }
    */

    
    //These two blocks of code were inside the Update() function and are linked to their respective functions they call
        
    /*   
    if (takeHit)
        {
            StartCoroutine(FadeTo(0.0f, 1.0f));
            StartCoroutine(FadeTo(1.0f, 1.0f));
            StartCoroutine(FadeTo(0.0f, 1.0f));
            StartCoroutine(FadeTo(1.0f, 1.0f));
        }
        */
    /*
    if(CurrentHealth < maxHealth && Time.deltaTime >= regenCooldown)
        {
            RegenerateHealth();
        }
    */
}
