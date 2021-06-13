using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // The number of players in game + 1 because player 0 doesn't make sense.
    public static int players = 5;

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
    public static Hands[] hands = new Hands[players];
    public static bool[] pickUpPressed = new bool[players];
    public static bool[] dropPressed = new bool[players];
    public static bool[] hasDestroyTrashPowerUp = new bool[players];
    public static bool[] facingLeft = new bool[players];

    public bool canPickUp1;
    public bool canPickUp2;
    public bool canPickUp3;
    public bool canPickUp4;
    public bool canMove;
    GameObject PowerUp;

    // Variables for throwing mechanics
    public static bool player1ThrowPressed;
    public static bool player2ThrowPressed;
    public static bool player3ThrowPressed;
    public static bool player4ThrowPressed;

    public static float player1ThrowPower = 20.0f;
    public static float player2ThrowPower = 20.0f;
    public static float player3ThrowPower = 20.0f;
    public static float player4ThrowPower = 20.0f;

    public AudioSource SFXPowerUp;

    public GameObject spawner;
    private TrashSpawn trashSpawn;
    public GameObject floatingSprint;
    public GameObject floatingThrow;
    public GameObject floatingDestroy;
    public GameObject floatingFreeze;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        spawner = GameObject.Find("TrashSpawner");
        trashSpawn = spawner.GetComponent<TrashSpawn>();
        for (int i = 0; i < players; i++) {
            pickUpPressed[i] = false;
            dropPressed[i] = false;
            hasDestroyTrashPowerUp[i] = false;
            facingLeft[i] = true;
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();

        switch(gameObject.tag)
        {
            case "Player1":
                if (movementInput.x < 0) {
                    facingLeft[1] = true;
                }
                else if (movementInput.x > 0) {
                    facingLeft[1] = false;
                }
                break;
            case "Player2":
                if (movementInput.x < 0) {
                    facingLeft[2] = true;
                }
                else if (movementInput.x > 0) {
                    facingLeft[2] = false;
                }
                break;
            case "Player3":
                if (movementInput.x < 0) {
                    facingLeft[3] = true;
                }
                else if (movementInput.x > 0) {
                    facingLeft[3] = false;
                }
                break;
            case "Player4":
                if (movementInput.x < 0) {
                    facingLeft[4] = true;
                }
                else if (movementInput.x > 0) {
                    facingLeft[4] = false;
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
                pickUpPressed[1] = context.action.triggered;
                break;
            case "Player2":
                pickUpPressed[2] = context.action.triggered;
                break;
            case "Player3":
                pickUpPressed[3] = context.action.triggered;
                break;
            case "Player4":
                pickUpPressed[4] = context.action.triggered;
                break;
        }
    }

    public void OnDrop(InputAction.CallbackContext context) {
        switch(gameObject.tag)
        {
            case "Player1":
                dropPressed[1] = context.action.triggered;
                break;
            case "Player2":
                dropPressed[2] = context.action.triggered;
                break;
            case "Player3":
                dropPressed[3] = context.action.triggered;
                break;
            case "Player4":
                dropPressed[4] = context.action.triggered;
                break;
        }
    }

    public void OnThrow(InputAction.CallbackContext context) {
        switch(gameObject.tag)
        {
            case "Player1":
                player1ThrowPressed = context.action.triggered;
                break;
            case "Player2":
                player2ThrowPressed = context.action.triggered;
                break;
            case "Player3":
                player3ThrowPressed = context.action.triggered;
                break;
            case "Player4":
                player4ThrowPressed = context.action.triggered;
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

            // Separate the controller movements for when players pick up the powerup
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
                (  canPickUp1 && pickUpPressed[1] 
                || canPickUp2 && pickUpPressed[2]
                || canPickUp3 && pickUpPressed[3]
                || canPickUp4 && pickUpPressed[4]) 
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

                    case "ThrowPowerUp":
                        StartCoroutine(ThrowPowerUp());
                        Destroy(PowerUp);
                        break;

                    case "FreezePowerUp":
                        StartCoroutine(FreezePowerUp());
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
        SFXPowerUp.Play();
        Instantiate(floatingSprint, transform.position, Quaternion.identity);
        switch (gameObject.tag)
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
        SFXPowerUp.Play();
        Instantiate(floatingDestroy, transform.position, Quaternion.identity);
        switch (gameObject.tag)
        {
            case "Player1":
                hasDestroyTrashPowerUp[1] = true;        // Set the bool to true in PickUp script so that trash can be destroyed
                yield return new WaitForSeconds(15);                // Wait for 15 seconds
                hasDestroyTrashPowerUp[1] = false;       // Reset the bool to false to end the power up
                break;
            case "Player2":
                hasDestroyTrashPowerUp[2] = true;        // Set the bool to true in PickUp script so that trash can be destroyed
                yield return new WaitForSeconds(15);                // Wait for 15 seconds
                hasDestroyTrashPowerUp[2] = false;       // Reset the bool to false to end the power up
                break;
            case "Player3":
                hasDestroyTrashPowerUp[3] = true;        // Set the bool to true in PickUp script so that trash can be destroyed
                yield return new WaitForSeconds(15);                // Wait for 15 seconds
                hasDestroyTrashPowerUp[3] = false;       // Reset the bool to false to end the power up
                break;
            case "Player4":
                hasDestroyTrashPowerUp[4] = true;        // Set the bool to true in PickUp script so that trash can be destroyed
                yield return new WaitForSeconds(15);                // Wait for 15 seconds
                hasDestroyTrashPowerUp[4] = false;       // Reset the bool to false to end the power up
                break;
            default:
                break;
        }
    }

     // Coroutine to execute the throw power up
    IEnumerator ThrowPowerUp()
    {
        SFXPowerUp.Play();
        Instantiate(floatingThrow, transform.position, Quaternion.identity);
        switch (gameObject.tag)
        {
            case "Player1":
                player1ThrowPower = 50f;                         // Increase the movement speed from 7 to 12
                yield return new WaitForSeconds(10);        // Wait for 10 seconds
                player1ThrowPower = 20f;                          // Return movement speed back to normal once 10 seconds have elapsed
                break;
            case "Player2":
                player2ThrowPower = 50f;                        // Increase the movement speed from 7 to 12
                yield return new WaitForSeconds(10);        // Wait for 10 seconds
                player2ThrowPower = 20f;                           // Return movement speed back to normal once 10 seconds have elapsed
                break;
            case "Player3":
                player3ThrowPower = 50f;                      // Increase the movement speed from 7 to 12
                yield return new WaitForSeconds(10);        // Wait for 10 seconds
                player3ThrowPower = 20f;                          // Return movement speed back to normal once 10 seconds have elapsed
                break;
            case "Player4":
                player4ThrowPower = 50f;                         // Increase the movement speed from 7 to 12
                yield return new WaitForSeconds(10);        // Wait for 10 seconds
                player4ThrowPower = 20f;                         // Return movement speed back to normal once 10 seconds have elapsed
                break;
        }
    }

    IEnumerator FreezePowerUp()
    {

        SFXPowerUp.Play();
        Instantiate(floatingFreeze, transform.position, Quaternion.identity);
        trashSpawn.isSpawning = false;
        canMove = false;
        yield return new WaitForSeconds(10);
        trashSpawn.isSpawning = true;
        canMove = true;
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

    private void OnTriggerExit(Collider other)
    {
            switch(gameObject.tag)
            {
                case "Player1":
                    Debug.Log("Player 1 exited power up trigger");
                    canPickUp1 = false;
                    break;
                case "Player2":
                    Debug.Log("Player 2 exited power up trigger");
                    canPickUp2 = false;
                    break;
                case "Player3":
                    Debug.Log("Player 3 exited power up trigger");
                    canPickUp3 = false;
                    break;
                case "Player4":
                    Debug.Log("Player 4 exited power up trigger");
                    canPickUp4 = false;
                    break;
                default:
                    break;
                    
            }
    }
}

