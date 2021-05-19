using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // [SerializeField]
    public static float playerSpeed = 7.0f;
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
    public static bool pickedup;
    public static bool dropped;
    public bool canPickUp;
    GameObject PowerUp;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        //hasItem = false;
        pickedup = false;
        dropped = false;
        //nearItem = false;
        //item = null;
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();
        //Debug.Log(movementInput);
    }

    public void OnJump(InputAction.CallbackContext context) {
        //Debug.Log("Jumping");
        //Debug.Log(groundedPlayer);
        jumped = context.action.triggered;
    }

    public void OnPickup(InputAction.CallbackContext context) {
        pickedup = context.action.triggered;
    }

    public void OnDrop(InputAction.CallbackContext context) {
        dropped = context.action.triggered;
    }

    void Update()
    {
        // anim.SetFloat("moveSpeed", playerVelocity.magnitude); // How fast is player moving?
        // If the player is grounded and it is not jumping
        groundedPlayer = controller.isGrounded;
        if(groundedPlayer && playerVelocity.y < 0)
        {
            // Keep the player grounded
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed); 

        // Running Animation
        if((move.x != 0) || (move.z != 0)) { // Note Y-Axis is always 0
            anim.SetBool("isRunning", true);  
        }
        else {
            anim.SetBool("isRunning", false);
        }

        // Flipping Orientation
        if(!theSR.flipX && move.x < 0)
        {
            theSR.flipX = true;
            // flipAnim.SetTrigger("Flip");
        } else if(theSR.flipX && move.x > 0) {
            theSR.flipX = false;
            // flipAnim.SetTrigger("Flip");
        }

        if(playerVelocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
            // Debug.Log(playerVelocity.y);
        }

        if(playerVelocity.y > 0)
        {
            anim.SetBool("isJumping", true);
        }

        if(playerVelocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }

        // Changes the height position of the player..
        if(jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        if(canPickUp && PowerUp != null && pickedup)
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

    // Coroutine to execute the sprint power up
    IEnumerator SprintPowerUp()
    {
        playerSpeed = 12f;                            // Increase the movement speed from 7 to 12
        yield return new WaitForSeconds(10);        // Wait for 10 seconds
        playerSpeed = 7f;                             // Return movement speed back to normal once 10 seconds have elapsed
    }

    // Coroutine to execute the destroy trash power up. 
    IEnumerator DestroyTrashPowerUp()
    {
        PickUp.hasDestroyTrashPowerUp = true;       // Set the bool to true in PickUp script so that trash can be destroyed
        yield return new WaitForSeconds(15);        // Wait for 15 seconds
        PickUp.hasDestroyTrashPowerUp = false;      // Reset the bool to false to end the power up
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            Debug.Log("Collided with power up");
            canPickUp = true;
            PowerUp = other.gameObject;
        }
    }
}

