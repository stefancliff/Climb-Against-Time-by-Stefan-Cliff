using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Diagnostics;
using TMPro;
using System.Numerics;

public class PlayerMovement : MonoBehaviour
{ 
    [Header("Movement Parameters")]
    [SerializeField] public float moveSpeed  = 5f;
    [SerializeField] private float jumpForce = 8.5f;
    [SerializeField] private float fallSpeed = 5f;
    /* [SerializeField] private float acceleration = 10f;
    private UnityEngine.Vector3 currentVelocity; */

    private Rigidbody2D     playerCharacter;
    private Animator        playerAnimator;
    private SpriteRenderer  spriteRenderer;
    private BoxCollider2D   playerCollider2D;
    
    private enum MovementState { idle, running, jumping, falling } // 0 -> idle; 1 -> running; 2 -> jumping; 3 -> falling;
    private MovementState state;
    [Space]
    [Header("Collision Detection with the Ground")]
    [SerializeField] private LayerMask terrainLayer;
    
    void Start()
    {
        playerCharacter     = GetComponent<Rigidbody2D>();
        playerAnimator      = GetComponent<Animator>();
        spriteRenderer      = GetComponent<SpriteRenderer>();
        playerCollider2D    = GetComponent<BoxCollider2D>();
        terrainLayer        = GetComponent<LayerMask>();
        
        /*
        From when I was trying to alter the gravity of the game itself, instead I found a different way to solve the 
        'floaty' feeling when it came to movement
        if(PlayerCharacter != null)
        {
            PlayerCharacter.gravityScale = gravity;
            UnityEngine.Debug.Log("Player Character Gravity = " + PlayerCharacter.gravityScale);

        }
        else
        {
            UnityEngine.Debug.Log("Player Character Missing");
        }
        */
    }

    void Update()
    {
        PlayerInput();
        //UnityEngine.Debug.Log("Player Character Gravity = " + PlayerCharacter.gravityScale);
    }

    private void PlayerInput()
    {
        float moveInput              = Input.GetAxis("Horizontal"); //Using GetAxisRaw to allow the Player Characters movement to avoid the slip n slide feeling when you stop inputting
        bool  jumpInput              = Input.GetButtonDown("Jump");
        float joystickInputMagnitude = Mathf.Clamp(Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")), 0f, 1f);

        
        // Move Right
        if (moveInput > 0) 
        {
            playerCharacter.velocity = new UnityEngine.Vector2(moveSpeed * moveInput, playerCharacter.velocity.y); // The reason we don't set the Y input to 0 is to not kill any of the previous movement
            spriteRenderer.flipX     = false; // This will flip the sprite to face the correct way
            state = MovementState.running;
            playerAnimator.SetFloat("moveSpeed", joystickInputMagnitude);
        }

        // Move Left
        else if (moveInput < 0) 
        {
            playerCharacter.velocity = new UnityEngine.Vector2(moveSpeed * moveInput, playerCharacter.velocity.y);
            spriteRenderer.flipX     = true; 
            state = MovementState.running;
            playerAnimator.SetFloat("moveSpeed", joystickInputMagnitude);
        }
        
        // Idle
        else 
        {
            state = MovementState.idle;
        } 

        // Jumping
        if (jumpInput && isGrounded()) 
        {
            playerCharacter.velocity = new UnityEngine.Vector2(playerCharacter.velocity.x, jumpForce); // Similar to before, as to keep the momentum of the players input
            state = MovementState.jumping;
        }

        // Falling
        else if (playerCharacter.velocity.y < -0.1f) 
        {
            playerCharacter.velocity += UnityEngine.Vector2.up * Physics2D.gravity.y * (fallSpeed - 1) * Time.deltaTime;
            state = MovementState.falling;
        }

        playerAnimator.SetInteger("moveState", (int)state);
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
        return Physics2D.BoxCast(playerCollider2D.bounds.center, playerCollider2D.bounds.size, 0f, UnityEngine.Vector2.down, .1f, terrainLayer);

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
