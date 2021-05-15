using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public static bool canPickUp;
    public static bool hasItem;
    GameObject objToPickUp;
    public GameObject trashSpawn;
    private TrashSpawn spawner;
    public static bool left;
    public static bool right;
    public static bool hasDestroyTrashPowerUp;

    // Start is called before the first frame update
    void Start()
    {
        //player =  GetComponent<Rigidbody>();
        canPickUp = false;
        hasItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Pick up trash if hands are empty
        if (canPickUp == true)
        {
            if (PlayerController.pickedup)
            {
                // If the object to be picked up is trash, and the destroy trash power up is not activated, then pick it up
                if (objToPickUp != null && objToPickUp.layer == 8 && !hasDestroyTrashPowerUp)
                {
                    objToPickUp.GetComponent<Rigidbody>().isKinematic = true;
                    objToPickUp.GetComponent<Rigidbody>().useGravity = false;
                    objToPickUp.transform.position = gameObject.transform.position + new Vector3(2f, 0, 0);
                    objToPickUp.transform.parent = gameObject.transform;
                    hasItem = true;
                }
                // The object to be picked up is trash and the player has the destroy trash power up
                else if(objToPickUp != null && objToPickUp.layer == 8 && hasDestroyTrashPowerUp)
                {
                    Destroy(objToPickUp);
                    spawner = trashSpawn.GetComponent<TrashSpawn>();
                    spawner.trashCount--;
                }
                // The object to be picked up is a power up
                else if(objToPickUp != null && objToPickUp.layer == 7)
                {
                    if(objToPickUp.CompareTag("SprintPowerUp"))
                    {
                        Debug.Log("Sprint Power Up Started");
                        Destroy(objToPickUp);
                        //StartCoroutine(SprintPowerUp());
                    }
                }

            }
        }

        // Drop trash if holding trash
        if (PlayerController.dropped && hasItem == true) // Holding an item and the drop key is pressed
        {
            objToPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again
            objToPickUp.GetComponent<Rigidbody>().useGravity = true;
            objToPickUp.transform.parent = null; // make the object no be a child of the hands
            hasItem = false;
        }
    }

    // Coroutine to execute the sprint power up
    //IEnumerator SprintPowerUp()
    //{
    //    PlayerController.playerSpeed = 12f;         // Increase the movement speed from 7 to 12
    //    yield return new WaitForSeconds(10);        // Wait for 10 seconds
    //    PlayerController.playerSpeed = 7f;          // Return movement speed back to normal once 10 seconds have elapsed
    //}

    private void FixedUpdate()
    {
        // Shows the trash on the left side of the player when moving towards the left
        if (left && hasItem)
        {
                objToPickUp.transform.position = gameObject.transform.position +  new Vector3(-2f,0,0);
        }
        // Shows the trash on the right side of the player when moving towards the right
        if (right && hasItem)
        {
                objToPickUp.transform.position = gameObject.transform.position +  new Vector3(2f,0,0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object being collided with is trash, then it can be picked up
        if (other.gameObject.layer == 8 && !hasItem)
        {
            Debug.Log("Trash trigger enter");
            canPickUp = true;
            objToPickUp = other.gameObject;
        }
        // The object is a power up and the player is not holding anything, so pick it up
        else if(other.gameObject.layer == 7 && !hasItem)
        {
            Debug.Log("Power up trigger entered");
            canPickUp = true;
            objToPickUp = other.gameObject;
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("Trash trigger exit");
        canPickUp = false;
    }

}
