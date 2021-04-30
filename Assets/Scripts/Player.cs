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
  
    private int superJumpsRemaining = 0;
    private float mvmtSpeed = 7f;

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

        // Control the horizontal movement of the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    // FixedUpdate is called once every physics update (100 ps)
    private void FixedUpdate()
    {
        // Check if player is colliding with another object to determine if it's grounded. Object is always colliding with itself, so there is 1 collision
        // if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1)

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
        // If the other collision game obect layer is 7, then it is a coin. Destroy that collision's game object
        if(other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }

        //if (other.gameObject.layer == 8)
        //{
        //    Destroy(other.gameObject);
        //}

    }


}
