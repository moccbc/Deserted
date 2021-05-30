using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.player1ThrowPressed && Player1PickUp.hasItem1) {
            Debug.Log("Player 1 throwing!");
            GameObject PlayerObj = Player1PickUp.objToPickUp;
            Player1PickUp.objToPickUp = null;
            PlayerObj.GetComponent<Rigidbody>().isKinematic = false;
            PlayerObj.GetComponent<Rigidbody>().useGravity = true;
            if (Player1PickUp.left) {
                PlayerObj.GetComponent<Rigidbody>().velocity = new Vector3(-PlayerController.player1ThrowPower, 2.0f, 0);
            }
            else if (Player1PickUp.right) {
                PlayerObj.GetComponent<Rigidbody>().velocity = new Vector3(PlayerController.player1ThrowPower, 2.0f, 0);
            }
            PlayerObj.transform.parent = null;
            Player1PickUp.hasItem1 = false;
        }
        else if (PlayerController.player2ThrowPressed && Player2PickUp.hasItem2) {
            Debug.Log("Player 2 throwing!");
            GameObject PlayerObj = Player2PickUp.objToPickUp;
            Player2PickUp.objToPickUp = null;
            PlayerObj.GetComponent<Rigidbody>().isKinematic = false;
            PlayerObj.GetComponent<Rigidbody>().useGravity = true;
            if (Player2PickUp.left) {
                PlayerObj.GetComponent<Rigidbody>().velocity = new Vector3(-PlayerController.player2ThrowPower, 2.0f, 0);
            }
            else if (Player2PickUp.right) {
                PlayerObj.GetComponent<Rigidbody>().velocity = new Vector3(PlayerController.player2ThrowPower, 2.0f, 0);
            }
            PlayerObj.transform.parent = null;
            Player2PickUp.hasItem2 = false;
        }
        else if (PlayerController.player3ThrowPressed && Player3PickUp.hasItem3) {
            Debug.Log("Player 3 throwing!");
            GameObject PlayerObj = Player3PickUp.objToPickUp;
            Player3PickUp.objToPickUp = null;
            PlayerObj.GetComponent<Rigidbody>().isKinematic = false;
            PlayerObj.GetComponent<Rigidbody>().useGravity = true;
            if (Player3PickUp.left) {
                PlayerObj.GetComponent<Rigidbody>().velocity = new Vector3(-PlayerController.player3ThrowPower, 2.0f, 0);
            }
            else if (Player3PickUp.right) {
                PlayerObj.GetComponent<Rigidbody>().velocity = new Vector3(PlayerController.player3ThrowPower, 2.0f, 0);
            }
            PlayerObj.transform.parent = null;
            Player3PickUp.hasItem3 = false;
        }
        else if (PlayerController.player4ThrowPressed && Player4PickUp.hasItem4) {
            Debug.Log("Player 4 throwing!");
            GameObject PlayerObj = Player4PickUp.objToPickUp;
            Player4PickUp.objToPickUp = null;
            PlayerObj.GetComponent<Rigidbody>().isKinematic = false;
            PlayerObj.GetComponent<Rigidbody>().useGravity = true;
            if (Player4PickUp.left) {
                PlayerObj.GetComponent<Rigidbody>().velocity = new Vector3(-PlayerController.player4ThrowPower, 2.0f, 0);
            }
            else if (Player4PickUp.right) {
                PlayerObj.GetComponent<Rigidbody>().velocity = new Vector3(PlayerController.player4ThrowPower, 2.0f, 0);
            }
            PlayerObj.transform.parent = null;
            Player4PickUp.hasItem4 = false;
        }
    }
}
