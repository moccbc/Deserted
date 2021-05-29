using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // [SerializeField]
    public static float player1Speed = 7.0f;
    public static float player2Speed = 7.0f;
    public static float player3Speed = 7.0f;
    public static float player4Speed = 7.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    [SerializeField] // Dilly stuff!
    public Animator anim; 
    [SerializeField]
    public SpriteRenderer theSR;
    // End of Dilly Declared Vars

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    
    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    // Variables for picking up mechanics
    public static bool player1PickUpPressed;
    public static bool player2PickUpPressed;
    public static bool player3PickUpPressed;
    public static bool player4PickUpPressed;
    public static bool player1DropPressed;
    public static bool player2DropPressed;
    public static bool player3DropPressed;
    public static bool player4DropPressed;

    public bool canPickUp;
    public bool canPickUp1;
    public bool canPickUp2;
    public bool canPickUp3;
    public bool canPickUp4;
    public bool canMove;
    GameObject PowerUp;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log(movementInput);

        switch(gameObject.tag)
        {
            case "Player1":
                if (movementInput.x < 0) {
                    Player1PickUp.left = true;
                    Player1PickUp.right = false;
                }
                else if (movementInput.x > 0) {
                    Player1PickUp.left = false;
                    Player1PickUp.right = true;
                }
                break;
            case "Player2":
                if (movementInput.x < 0) {
                    Player2PickUp.left = true;
                    Player2PickUp.right = false;
                }
                else if (movementInput.x > 0) {
                    Player2PickUp.left = false;
                    Player2PickUp.right = true;
                }
                break;
            case "Player3":
                if (movementInput.x < 0) {
                    Player3PickUp.left = true;
                    Player3PickUp.right = false;
                }
                else if (movementInput.x > 0) {
                    Player3PickUp.left = false;
                    Player3PickUp.right = true;
                }
                break;
            case "Player4":
                if (movementInput.x < 0) {
                    Player4PickUp.left = true;
                    Player4PickUp.right = false;
                }
                else if (movementInput.x > 0) {
                    Player4PickUp.left = false;
                    Player4PickUp.right = true;
                }
                break;
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        jumped = context.action.triggered;
    }

    public void OnPickup(InputAction.CallbackContext context) {
        switch(gameObject.tag)
        {
            case "Player1":
                player1PickUpPressed = context.action.triggered;
                break;
            case "Player2":
                player2PickUpPressed = context.action.triggered;
                break;
            case "Player3":
                player3PickUpPressed = context.action.triggered;
                break;
            case "Player4":
                player4PickUpPressed = context.action.triggered;
                break;
        }
    }

    public void OnDrop(InputAction.CallbackContext context) {
        switch(gameObject.tag)
        {
            case "Player1":
                player1DropPressed = context.action.triggered;
                break;
            case "Player2":
                player2DropPressed = context.action.triggered;
                break;
            case "Player3":
                player3DropPressed = context.action.triggered;
                break;
            case "Player4":
                player4DropPressed = context.action.triggered;
                break;
        }
    }

    void Update()
    {
        // If the player is grounded and it is not jumping
        groundedPlayer = controller.isGrounded;
        if(groundedPlayer && playerVelocity.y < 0)
        {
            // Keep the player grounded
            playerVelocity.y = 0f;
        }

        //check to see if game over or pause menu has frozen movement
        if (canMove)
        {
            Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

            switch(gameObject.tag)
            {
                case "Player1":
                    controller.Move(move * Time.deltaTime * player1Speed);
                    break;
                case "Player2":
                    controller.Move(move * Time.deltaTime * player2Speed);
                    break;
                case "Player3":
                    controller.Move(move * Time.deltaTime * player3Speed);
                    break;
                case "Player4":
                    controller.Move(move * Time.deltaTime * player4Speed);
                    break;
                default:
                    break;
            }
            //controller.Move(move * Time.deltaTime * playerSpeed);

            // Running Animation
            if ((move.x != 0) || (move.z != 0))
            { // Note Y-Axis is always 0
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
            // Flipping Orientation
            if (!theSR.flipX && move.x < 0)
            {
                theSR.flipX = true;
                // flipAnim.SetTrigger("Flip");
            }
            else if (theSR.flipX && move.x > 0)
            {
                theSR.flipX = false;
                // flipAnim.SetTrigger("Flip");
            }
            if (playerVelocity.y == 0)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", false);
                // Debug.Log(playerVelocity.y);
            }

            if (playerVelocity.y > 0)
            {
                anim.SetBool("isJumping", true);
            }

            if (playerVelocity.y < 0)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", true);
            }

            // Changes the height position of the player..
            if (canMove && jumped && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            if (canMove && 
                (canPickUp1 && player1PickUpPressed 
                || canPickUp2 && player2PickUpPressed
                || canPickUp3 && player3PickUpPressed
                || canPickUp4 && player4PickUpPressed) 
                && PowerUp != null)
            {
                switch (PowerUp.tag)
                {
                    // The sprint power up was picked up
                    case "SprintPowerUp":
                        StartCoroutine(SprintPowerUp());    // Call coroutine to execute the power up for 15 seconds
                        Destroy(PowerUp);                   // Destroy the power up as soon as it's picked up
                        break;

                    // The Destroy Trash power up was picked up
                    case "DestroyTrashPowerUp":
                        StartCoroutine(DestroyTrashPowerUp());
                        Destroy(PowerUp);
                        break;
                }
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
        //if game over or pause menu has been activated, set animation isRunning to false
        else
        {
            anim.SetBool("isRunning", false);
        }
        
    }

    // Coroutine to execute the sprint power up
    IEnumerator SprintPowerUp()
    {
        switch(gameObject.tag)
        {
            case "Player1":
                player1Speed = 12f;                         // Increase the movement speed from 7 to 12
                yield return new WaitForSeconds(10);        // Wait for 10 seconds
                player1Speed = 7f;                          // Return movement speed back to normal once 10 seconds have elapsed
                break;
            case "Player2":
                player2Speed = 12f;                         // Increase the movement speed from 7 to 12
                yield return new WaitForSeconds(10);        // Wait for 10 seconds
                player2Speed = 7f;                          // Return movement speed back to normal once 10 seconds have elapsed
                break;
            case "Player3":
                player3Speed = 12f;                         // Increase the movement speed from 7 to 12
                yield return new WaitForSeconds(10);        // Wait for 10 seconds
                player3Speed = 7f;                          // Return movement speed back to normal once 10 seconds have elapsed
                break;
            case "Player4":
                player4Speed = 12f;                         // Increase the movement speed from 7 to 12
                yield return new WaitForSeconds(10);        // Wait for 10 seconds
                player4Speed = 7f;                          // Return movement speed back to normal once 10 seconds have elapsed
                break;
        }
    }

    // Coroutine to execute the destroy trash power up. 
    IEnumerator DestroyTrashPowerUp()
    {
        switch(gameObject.tag)
        {
            case "Player1":
                Player1PickUp.hasDestroyTrashPowerUp = true;        // Set the bool to true in PickUp script so that trash can be destroyed
                yield return new WaitForSeconds(15);                // Wait for 15 seconds
                Player1PickUp.hasDestroyTrashPowerUp = false;       // Reset the bool to false to end the power up
                break;
            case "Player2":
                Player2PickUp.hasDestroyTrashPowerUp = true;        // Set the bool to true in PickUp script so that trash can be destroyed
                yield return new WaitForSeconds(15);                // Wait for 15 seconds
                Player2PickUp.hasDestroyTrashPowerUp = false;       // Reset the bool to false to end the power up
                break;
            case "Player3":
                Player3PickUp.hasDestroyTrashPowerUp = true;        // Set the bool to true in PickUp script so that trash can be destroyed
                yield return new WaitForSeconds(15);                // Wait for 15 seconds
                Player3PickUp.hasDestroyTrashPowerUp = false;       // Reset the bool to false to end the power up
                break;
            case "Player4":
                Player4PickUp.hasDestroyTrashPowerUp = true;        // Set the bool to true in PickUp script so that trash can be destroyed
                yield return new WaitForSeconds(15);                // Wait for 15 seconds
                Player4PickUp.hasDestroyTrashPowerUp = false;       // Reset the bool to false to end the power up
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check that the object collided with is a powerup
        if(other.gameObject.layer == 7)
        {
            switch(gameObject.tag)
            {
                case "Player1":
                    Debug.Log("Player 1 collided with power up");
                    canPickUp1 = true;
                    PowerUp = other.gameObject;
                    break;
                case "Player2":
                    Debug.Log("Player 2 collided with power up");
                    canPickUp2 = true;
                    PowerUp = other.gameObject;
                    break;
                case "Player3":
                    Debug.Log("Player 3 collided with power up");
                    canPickUp3 = true;
                    PowerUp = other.gameObject;
                    break;
                case "Player4":
                    Debug.Log("Player 4 collided with power up");
                    canPickUp4 = true;
                    PowerUp = other.gameObject;
                    break;
                default:
                    break;
                    
            }
        }
    }
}

