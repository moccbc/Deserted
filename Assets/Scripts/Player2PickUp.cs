using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2PickUp : MonoBehaviour
{
    public static bool canPickUp2;
    public static bool hasItem2;
    GameObject objToPickUp;
    public static bool left;
    public static bool right;
    public static bool hasDestroyTrashPowerUp;
    public AudioSource SFXPickUp;
    public AudioSource SFXDrop;
    private bool SFXPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        canPickUp2 = false;
        hasItem2 = false;
        hasDestroyTrashPowerUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Pick up trash if hands are empty
        if (canPickUp2 == true)
        {
            if (PlayerController.player2PickUpPressed)
            {
                // If the object to be picked up is trash, and the destroy trash power up is not activated, then pick it up
                if (objToPickUp != null && objToPickUp.layer == 8 && !hasDestroyTrashPowerUp)
                {
                    objToPickUp.GetComponent<Rigidbody>().isKinematic = true;
                    objToPickUp.GetComponent<Rigidbody>().useGravity = false;
                    if (left)
                        objToPickUp.transform.position = gameObject.transform.position + new Vector3(-2f, 1.2f, 0);
                    else if (right)
                        objToPickUp.transform.position = gameObject.transform.position + new Vector3(2f, 1.2f, 0);
                    objToPickUp.transform.eulerAngles = new Vector3(0, 0, 0);
                    objToPickUp.transform.parent = gameObject.transform;
                    hasItem2 = true;

                    if(!SFXPlayed) {
                        SFXPickUp.Play();
                        SFXPlayed = true;
                    }
                }
                // The object to be picked up is trash and the player has the destroy trash power up
                else if (objToPickUp != null && objToPickUp.layer == 8 && hasDestroyTrashPowerUp)
                {
                    Destroy(objToPickUp);
                    TrashBin.trashPutAway++;
                }
            }
        }

        // Drop trash if holding trash
        if (PlayerController.player2DropPressed && hasItem2 == true) // Holding an item and the drop key is pressed
        {
            objToPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again
            objToPickUp.GetComponent<Rigidbody>().useGravity = true;
            objToPickUp.transform.parent = null; // make the object no be a child of the hands
            hasItem2 = false;
            SFXDrop.Play();
            SFXPlayed = false;
        }
    }

    private void FixedUpdate()
    {
        // Shows the trash on the left side of the player when moving towards the left
        if (left && hasItem2)
        {
            objToPickUp.transform.position = gameObject.transform.position + new Vector3(-2f, 1.2f, 0);
        }
        // Shows the trash on the right side of the player when moving towards the right
        if (right && hasItem2)
        {
            objToPickUp.transform.position = gameObject.transform.position + new Vector3(2f, 1.2f, 0);
        }
        // If the player was previously carrying trash, but is not any longer, reset hasItem to false
        if (gameObject.transform.childCount == 2)
        {
            hasItem2 = false;
            SFXPlayed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object being collided with is trash, then it can be picked up
        if (other.gameObject.layer == 8 && !hasItem2)
        {
            Debug.Log("Trash trigger enter");

            switch (gameObject.tag)
            {
                case "Player2":
                    Debug.Log("Player 2 can pick up");
                    canPickUp2 = true;
                    objToPickUp = other.gameObject;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        canPickUp2 = false;
    }
}
