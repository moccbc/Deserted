using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
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
    GameObject item;
    public static bool hasItem;
    public static bool pickedup;
    public static bool dropped;
    private bool nearItem;

    GameObject playerPrefab;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        hasItem = false;
        pickedup = false;
        dropped = false;
        nearItem = false;
        item = null;
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log(movementInput);
    }

    public void OnJump(InputAction.CallbackContext context) {
        Debug.Log("Jumping");
        Debug.Log(groundedPlayer);
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


        // This will transform the object when it moves
        // For example, rotation, flipping, etc.
        // Disabled for now so that it doesn't rotate player
        // if (move != Vector3.zero)
        // {
        //     gameObject.transform.forward = move;
        // }

        // Changes the height position of the player..
        if(jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // Picking up an item
        if (pickedup && !hasItem && nearItem) {
            Debug.Log("Picking up!");
            item.transform.position = gameObject.transform.position
                                    + new Vector3(2f, 0, 0);
            item.transform.parent = gameObject.transform;
            hasItem = true;
        }

        // Dropping an item
        if (dropped && hasItem) {
            // make the rigidbody work again
            item.GetComponent<Rigidbody>().isKinematic = false; 
            // make the object no be a child of the hands
            item.transform.parent = null; 
            item = null;
            hasItem = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.layer == 8 && !hasItem) {                
            Debug.Log("Hit a pickuppable!");
            nearItem = true;
            item = hit.gameObject;
        }
    }
}
