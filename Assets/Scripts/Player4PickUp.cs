using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4PickUp : MonoBehaviour
{
    public static bool canPickUp4;
    public static bool hasItem4;
    GameObject objToPickUp;
    public static bool left;
    public static bool right;
    public static bool hasDestroyTrashPowerUp;

    // Start is called before the first frame update
    void Start()
    {
        canPickUp4 = false;
        hasItem4 = false;
        hasDestroyTrashPowerUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Pick up trash if hands are empty
        if (canPickUp4 == true)
        {
            if (PlayerController.player4PickUpPressed)
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
                    hasItem4 = true;
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
        if (PlayerController.player4DropPressed && hasItem4 == true) // Holding an item and the drop key is pressed
        {
            objToPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again
            objToPickUp.GetComponent<Rigidbody>().useGravity = true;
            objToPickUp.transform.parent = null; // make the object no be a child of the hands
            hasItem4 = false;
        }
    }

    private void FixedUpdate()
    {
        // Shows the trash on the left side of the player when moving towards the left
        if (left && hasItem4)
        {
            objToPickUp.transform.position = gameObject.transform.position + new Vector3(-2f, 1.2f, 0);
        }
        // Shows the trash on the right side of the player when moving towards the right
        if (right && hasItem4)
        {
            objToPickUp.transform.position = gameObject.transform.position + new Vector3(2f, 1.2f, 0);
        }
        // If the player was previously carrying trash, but is not any longer, reset hasItem to false
        if (gameObject.transform.childCount == 2)
        {
            hasItem4 = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object being collided with is trash, then it can be picked up
        if (other.gameObject.layer == 8 && !hasItem4)
        {
            Debug.Log("Trash trigger enter");

            switch (gameObject.tag)
            {
                case "Player4":
                    Debug.Log("Player 4 can pick up");
                    canPickUp4 = true;
                    objToPickUp = other.gameObject;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        canPickUp4 = false;
    }
}
