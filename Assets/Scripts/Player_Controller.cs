using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {
    public float moveSpeed, jumpForce; 
    private Vector2 moveInput;
    public Vector3 jump;
    public LayerMask groundLayer;

    private bool isGrounded;
    public Rigidbody rb;

    public SphereCollider col;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>(); 
    }

    // Update is called once per frame
    void Update() {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        rb.velocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y, moveInput.y * moveSpeed);

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded() {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayer);
    }    
}
