using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Set to max players you want plus 1
    // The first entry will not be used because player 0
    // doesn't make sense
    public static int players = 5;
    public static bool[] canPickUp  = new bool[players];
    public static bool[] hasItem = new bool[players];
    public static GameObject[] objects = new GameObject[players];
    public static bool[] facingLeft  = new bool[players];
    public static bool[] facingRight = new bool[players];
    public static bool[] hasDestroyTrashPowerUp = new bool[players];
    public static CharacterController[] controllers = new CharacterController[players];


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < players; i++) {
            canPickUp[i] = false;
            hasItem[i] = false;
            facingLeft[i] = true;
            facingRight[i] = false;
            hasDestroyTrashPowerUp[i] = false;
            objects[i] = null;
        }
        switch(gameObject.tag) {
            case "Player1":
                controllers[1] = gameObject.GetComponent<CharacterController>();
                break;
            case "Player2":
                controllers[2] = gameObject.GetComponent<CharacterController>();
                break;
            case "Player3":
                controllers[3] = gameObject.GetComponent<CharacterController>();
                break;
            case "Player4":
                controllers[4] = gameObject.GetComponent<CharacterController>();
                break;
            default:
                break;
        }
    }

    void grabItem(int player) {
        for (int i = 0; i < players; i++) Debug.Log(hasItem[i]);
        Debug.Log("Player " + player + " grabbing item");
        // Pick up trash if hands are empty
        if (canPickUp[player])
        {
            Debug.Log("Player " + player + " can pickup");
            // If the object to be picked up is trash, and the destroy trash power up is not activated, then pick it up
            if (objects[player] != null && objects[player].layer == 8 && !hasDestroyTrashPowerUp[player])
            {
                Debug.Log(gameObject);
                Debug.Log(gameObject.tag);
                objects[player].GetComponent<Rigidbody>().isKinematic = true;
                objects[player].GetComponent<Rigidbody>().useGravity = false;
                if (facingLeft[player])
                    objects[player].transform.position = controllers[player].transform.position + new Vector3(-2f, 1.2f, 0);
                else if (facingRight[player])
                    objects[player].transform.position = controllers[player].transform.position + new Vector3(2f, 1.2f, 0);
                objects[player].transform.eulerAngles = new Vector3(0, 0, 0);
                objects[player].transform.parent = controllers[player].transform;
                hasItem[player] = true;
            }
            // The object to be picked up is trash and the player has the destroy trash power up
            else if(objects[player] != null && objects[player].layer == 8 && hasDestroyTrashPowerUp[player])
            {
                Destroy(objects[player]);
                TrashBin.trashPutAway++;
            }
        }
    }

    void dropItem(int player) {
        if (hasItem[player] == true) // Holding an item and the drop key is pressed
        {
            objects[player].GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again
            objects[player].GetComponent<Rigidbody>().useGravity = true;
            objects[player].transform.parent = null; // make the object no be a child of the hands
            hasItem[player] = false;
            objects[player] = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerController.player1PickUpPressed) {
            grabItem(1);
        }
        else if (PlayerController.player2PickUpPressed) {
            grabItem(2);
        }
        //else if (PlayerController.player3PickUpPressed) {
        //    grabItem(3);
        //}
        //else if (PlayerController.player4PickUpPressed) {
        //    grabItem(4);
        //}

        if (PlayerController.player1DropPressed) {
            dropItem(1);
        }
        else if (PlayerController.player2DropPressed) {
            dropItem(2);
        }
        //else if (PlayerController.player3DropPressed) {
        //    dropItem(3);
        //}
        //else if (PlayerController.player4DropPressed) {
        //    dropItem(4);
        //}
    }

    private void faceLeftRight(int player) {
        if (hasItem[player]) {
            // Shows the trash on the left side of the player when moving towards the left
            if (facingLeft[player]) {
                objects[player].transform.position = controllers[player].transform.position + new Vector3(-2f,1.2f,0);
            }
            // Shows the trash on the right side of the player when moving towards the right
            else if (facingRight[player]){
                objects[player].transform.position = controllers[player].transform.position + new Vector3(2f,1.2f,0);
            }
        }
    }

    private void FixedUpdate()
    {
        faceLeftRight(1); //faceLeftRight(2); faceLeftRight(3); faceLeftRight(4);

        // If the player was previously carrying trash, but is not any longer, reset hasItem to false
        //if (gameObject.transform.childCount == 2)
        //{
        //    hasItem[1] = false;
        //    hasItem[2] = false;
        //    hasItem[3] = false;
        //    hasItem[4] = false;
        //}
    }

    void setItem(int player, Collider other) {
        // If the object being collided with is trash, then it can be picked up
        if (other.gameObject.layer == 8 && !hasItem[player])
        {
            Debug.Log("Trash trigger enter");
            Debug.Log("Player " + player + " can pick up");
            canPickUp[player] = true;
            objects[player] = other.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(gameObject.tag) {
            case "Player1":
                setItem(1, other);
                break;
            //case "Player2":
            //    setItem(2, other);
            //    break;
            //case "Player3":
            //    setItem(3, other);
            //    break;
            //case "Player4":
            //    setItem(4, other);
            //    break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        switch(gameObject.tag) {
            case "Player1":
                canPickUp[1] = false;
                break;
            //case "Player2":
            //    canPickUp[2] = false;
            //    break;
            //case "Player3":
            //    canPickUp[3] = false;
            //    break;
            //case "Player4":
            //    canPickUp[4] = false;
            //    break;
            default:
                break;
        }
    }

    
}

