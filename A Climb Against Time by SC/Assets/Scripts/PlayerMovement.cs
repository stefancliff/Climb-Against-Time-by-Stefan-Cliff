using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public int maxHealth = 3;
    public float damageFallHeight = 5f;
    public float regenDelay = 30f;
    public float levelTime = 180f; // 3 minutes

    public Image timerFillImage; // Reference to the UI timer fill image

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = false;
    private int health;
    private float lastDamageTime;
    private float currentTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = maxHealth;
        lastDamageTime = Time.time - regenDelay;
        currentTime = levelTime;
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Get input for horizontal movement
        float move = Input.GetAxis("Horizontal");

        // Move the player horizontally
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Flip the player sprite depending on movement direction
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Check for jump input and jump if grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Update animator parameters
        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetBool("IsGrounded", isGrounded);

        // Check for fall damage
        if (transform.position.y < -damageFallHeight)
        {
            TakeDamage();
        }

        // Regenerate health after delay
        if (Time.time - lastDamageTime >= regenDelay && health < maxHealth)
        {
            health++;
            lastDamageTime = Time.time;
        }

        // Update the timer
        UpdateTimer();
    }

    void TakeDamage()
    {
        health--;
        lastDamageTime = Time.time;

        // TODO: Play damage animation and sound effect

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // TODO: Play death animation and sound effect
        // TODO: Fade to black effect

        // Restart the level after a delay
        Invoke("RestartLevel", 1f);
    }

    void RestartLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        // Update the UI timer fill image
        timerFillImage.fillAmount = currentTime / levelTime;

        if (currentTime <= 0)
        {
            // TODO: Fade to black effect
            // Restart the level after a delay
            Invoke("RestartLevel", 1f);
        }
    }
}
