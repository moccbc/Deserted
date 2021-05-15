using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [SerializeField] private Transform groundCheckTransform; // SerializedField shows the Transform in the inspector in Unity
    [SerializeField] private LayerMask playerMask;

    public bool movingLeft;
    public bool movingRight;
    private bool jumpKeyPressed = false;
    private float horizontalInput;
    private float verticalInput;
    public bool canPickUp;
    GameObject PowerUp;
    private int superJumpsRemaining = 0;
    public float mvmtSpeed = 7f;

    private Rigidbody rigidbodyComponent; 

    // Start is called before the first frame update
    void Start(){
        rigidbodyComponent = GetComponent<Rigidbody>();
        movingLeft = false;
        movingRight = false;
    }

    // Update is called once per frame 
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            jumpKeyPressed = true;
        }

        if (Input.GetKeyDown("a"))
        {
            PickUp.left = true;
            PickUp.right = false;
        }

        if (Input.GetKeyDown("d"))
        {
            PickUp.right = true;
            PickUp.left = false;
        }

        // Mechanics to pick up power ups
        if(canPickUp && PowerUp != null && Input.GetKeyDown("e"))
        {
            switch(PowerUp.tag)
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

        // Control the horizontal movement of the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    // Coroutine to execute the sprint power up
    IEnumerator SprintPowerUp()
    {
        mvmtSpeed = 12f;                            // Increase the movement speed from 7 to 12
        yield return new WaitForSeconds(10);        // Wait for 10 seconds
        mvmtSpeed = 7f;                             // Return movement speed back to normal once 10 seconds have elapsed
    }

    // Coroutine to execute the destroy trash power up. 
    IEnumerator DestroyTrashPowerUp()
    {
        PickUp.hasDestroyTrashPowerUp = true;       // Set the bool to true in PickUp script so that trash can be destroyed
        yield return new WaitForSeconds(15);        // Wait for 15 seconds
        PickUp.hasDestroyTrashPowerUp = false;      // Reset the bool to false to end the power up
    }

    // FixedUpdate is called once every physics update (100 ps)
    private void FixedUpdate()
    {
        // Add the horizontal component to the x-axis
        rigidbodyComponent.velocity = new Vector3(horizontalInput * mvmtSpeed, rigidbodyComponent.velocity.y, verticalInput * mvmtSpeed);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyPressed)
        {
            float jumpPower = 5f;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }

            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the other collision game obect layer is 7, then it is a power up and it can be picked up
        if (other.gameObject.layer == 7)
        {
            Debug.Log("Collided with power up");
            canPickUp = true;
            PowerUp = other.gameObject;
        }

    }
}
