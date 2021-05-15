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

    GameObject playerPrefab;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        // anim = gameObject.GetComponent<Animator>(); 
    }

    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) {
        Debug.Log("Jumping");
        Debug.Log(groundedPlayer);
        jumped = context.action.triggered;
    }

    void Update()
    {
        // anim.SetFloat("moveSpeed", playerVelocity.magnitude); // How fast is player moving?
        groundedPlayer = controller.isGrounded;
        if(groundedPlayer && playerVelocity.y < 0)
        {
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

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
