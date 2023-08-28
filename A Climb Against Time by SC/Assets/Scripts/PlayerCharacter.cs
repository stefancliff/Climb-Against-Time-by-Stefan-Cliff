using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Diagnostics;
using TMPro;

public class PlayerMovement : MonoBehaviour
{ 
        
    public float moveSpeed  = 5f;
    public float jumpForce  = 10f;
    public float levelTimer = 90f; // 1.5 min
    public float gravity    = 1f;

   // public Image timerFillImage; // Reference to the UI timer fill image

    private Rigidbody2D     PlayerCharacter;
    private Animator        PlayerAnimator;
    private SpriteRenderer  spriteRenderer;
    private BoxCollider2D   PlayerCollider2D;
    
    
    private enum MovementState { idle, running, jumping, falling } // 0 -> idle; 1 -> running; 2 -> jumping; 3 -> falling;
    private MovementState state;
    [SerializeField] private LayerMask terrainLayer;
    
    void Start()
    {
        PlayerCharacter     = GetComponent<Rigidbody2D>();
        PlayerAnimator      = GetComponent<Animator>();
        spriteRenderer      = GetComponent<SpriteRenderer>();
        PlayerCollider2D    = GetComponent<BoxCollider2D>();
        terrainLayer        = GetComponent<LayerMask>();
        
        if(PlayerCharacter != null)
        {
            PlayerCharacter.gravityScale = gravity;
            UnityEngine.Debug.Log("Player Character Gravity = " + PlayerCharacter.gravityScale);

        }
        else
        {
            UnityEngine.Debug.Log("Player Character Missing");
        }
    }

    void Update()
    {
        PlayerInput();
        levelTimer -= Time.deltaTime;
        UnityEngine.Debug.Log("Player Character Gravity = " + PlayerCharacter.gravityScale);
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

 /* 
    private void CheckFallDamage(MovementState state)
    {
        if (state == MovementState.falling)
        {
            // Increment fallTimer by Time.deltaTime
            fallTimer += Time.deltaTime;

            if (fallTimer >= 2f && fallTimer < 4f)
            {
                TakeDamage(1);
            }
            else if (fallTimer >= 4f && fallTimer < 6f)
            {
                TakeDamage(2);
            }
            else if (fallTimer >= 6f)
            {
                TakeDamage(3);
            }
        }
        else
        {
            // Reset fallTimer if not falling
            fallTimer = 0f;
        }
} */


}
