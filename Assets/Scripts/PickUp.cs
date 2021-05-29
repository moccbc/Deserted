using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public static bool canPickUp;
    public static bool hasItem;
    GameObject objToPickUp;
    public static bool left;
    public static bool right;
    public static bool hasDestroyTrashPowerUp;
    public AudioSource SFXPickUp;
    public AudioClip SFXPU;
    public AudioSource SFXDrop;
    private bool SFXPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        canPickUp = false;
        hasItem = false;
        hasDestroyTrashPowerUp = false;
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

                    if(!SFXPlayed) {
                        SFXPickUp.PlayOneShot(SFXPU);
                        SFXPlayed = true;
                    }
                     // Play Pick Up Sound effect!
                }
                // The object to be picked up is trash and the player has the destroy trash power up
                else if(objToPickUp != null && objToPickUp.layer == 8 && hasDestroyTrashPowerUp)
                {
                    Destroy(objToPickUp);
                    TrashBin.trashPutAway++;
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
            SFXDrop.Play(); // Play Drop Sound Effect!
            SFXPlayed = false;
        }
    }

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
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("Trash trigger exit");
        canPickUp = false;
        SFXPlayed = false;
    }

}
