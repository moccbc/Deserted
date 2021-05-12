using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    
    private bool canPickUp;
    public static bool hasItem;
    GameObject trash;
    public static bool left;
    public static bool right;
    private float radius = 12f;

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
            if (Input.GetKeyDown("e"))
            {
                //trash.GetComponent<Rigidbody>().isKinematic = true;
                trash.GetComponent<Rigidbody>().useGravity = false;
                trash.transform.position = gameObject.transform.position +  new Vector3(2f,0,0);
                trash.transform.parent = gameObject.transform;
                hasItem = true;

            }
        }

        // Drop trash if holding trash
        if (Input.GetKeyDown("q") && hasItem == true) // if you have an item and get the key to remove the object, again can be any key
        {
            trash.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again
            trash.transform.parent = null; // make the object no be a child of the hands
            hasItem = false;
        }
    }
    private void FixedUpdate()
    {
        // Shows the trash on the left side of the player when moving towards the left
        if (left && hasItem)
        {
                trash.transform.position = gameObject.transform.position +  new Vector3(-2f,0,0);
        }
        // Shows the trash on the right side of the player when moving towards the right
        if (right && hasItem)
        {
                trash.transform.position = gameObject.transform.position +  new Vector3(2f,0,0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object being collided with is trash, then it can be picked up
        if (other.gameObject.layer == 8 && !hasItem)
        {
            //Debug.Log("Hit the trash");
            canPickUp = true;
            trash = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("");
        canPickUp = false;
    }

}
