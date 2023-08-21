using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{ 
        
    public float    moveSpeed           = 5f;
    public float    jumpForce           = 10f;
    public int      maxHealth           = 3;
    public float    fallTimer      = 0f;
    public float    regenerationDelay   = 30f; // seconds till health regeneration begins
    public float    levelTimer          = 180f; // 3 minutes

   // public Image timerFillImage; // Reference to the UI timer fill image

    private Rigidbody2D     PlayerCharacter;
    private Animator        PlayerAnimator;
    private SpriteRenderer  spriteRenderer;
    private BoxCollider2D   PlayerCollider2D;
    [SerializeField] private LayerMask terrainLayer;

    private enum MovementState { idle, running, jumping, falling } // 0 -> idle; 1 -> running; 2 -> jumping; 3 -> falling;
    private MovementState state;

    private int     CurrentHealth;
    private float   lastDamageTime;
    private float   currentTime;
    private float   fallDuration = 0f;
    private bool    isDead = false;


    void Start()
    {
        PlayerCharacter     = GetComponent<Rigidbody2D>();
        PlayerAnimator      = GetComponent<Animator>();
        spriteRenderer      = GetComponent<SpriteRenderer>();
        PlayerCollider2D    = GetComponent<BoxCollider2D>();
        terrainLayer        = GetComponent<LayerMask>();
        CurrentHealth       = maxHealth;
        isDead              = false;

    }

    void Update()
    {
        PlayerInput();
        
    }

    private void PlayerInput()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); //Using GetAxisRaw to allow the Player Characters movement to avoid the slip n slide feeling when you stop inputting
        bool jumpInput  = Input.GetButtonDown("Jump");
        
        // Move Right
        if (moveInput > 0) 
        {
            PlayerCharacter.velocity = new Vector2(moveSpeed * moveInput, PlayerCharacter.velocity.y); // The reason we don't set the Y input to 0 is to not kill any of the previous movement
            spriteRenderer.flipX = false; // This will flip the sprite to face the correct way
            state = MovementState.running;
        }

        // Move Left
        else if (moveInput < 0) 
        {
            PlayerCharacter.velocity = new Vector2(moveSpeed * moveInput, PlayerCharacter.velocity.y);
            spriteRenderer.flipX = true; 
            state = MovementState.running;
        }
        
        // Idle
        else 
        {
            state = MovementState.idle;
        } 

        // Jumping
        if(jumpInput && isGrounded()) 
        {
            PlayerCharacter.velocity = new Vector2(PlayerCharacter.velocity.x, jumpForce); // Similar to before, as to keep the momentum of the players input
            state = MovementState.jumping;
        }

        // Falling
        else if(PlayerCharacter.velocity.y < -0.1f) 
        {
            state = MovementState.falling;
            CheckFallDamage();
        }

        PlayerAnimator.SetInteger("moveState", (int)state);
    }

    private bool isGrounded()
    {
        /* 
            Using BoxCast as a way to nicely detect the players touching of the wall: How we do it is as follows really:

                PlayerCollider2D.bounds.center   -> is where we essentially drawn another box around the players box collider using the first ones center as a reference
                PlayerCollider2D.bounds.size     -> so that the sizes match
                0f                               -> The angle/rotation of our BoxCast (0 means default/normal)
                Vector2.down and the .1f         -> they move this BoxCast we created down just a little bit so that we can actually check the overlapping of 
                                                    the players new collision box with the Terrains collision box
                terrainLayer                     -> this is what we are comparing our new BoxCast collider with, in this case the ground of the terrain   
        */
        return Physics2D.BoxCast(PlayerCollider2D.bounds.center, PlayerCollider2D.bounds.size, 0f, Vector2.down, .1f, terrainLayer);

    }

    private void CheckFallDamage()
    {
        fallTimer = 0f;                // First we reset the fallTimer to 0 
        
        while(state == MovementState.falling)
        {
            fallTimer = Time.deltaTime;    // Then as each second passes we increase keep track of it
            
            if(fallTimer >= 2f && fallTimer < 4f && state != MovementState.falling){

                TakeDamage(1);
            }
            else if(fallTimer >= 4f && fallTimer < 6f && state != MovementState.falling)
            {
                TakeDamage(2);
            }
            else if(fallTimer >= 6f && state != MovementState.falling){
                TakeDamage(3);
            }

        }
        fallTimer = 0f;
        
    }

    private void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, 3);

        if(CurrentHealth <= 0)
        {
            isDead = true;
        }
    }

    
}
