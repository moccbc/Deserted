using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void throwItem(int player, GameObject[] PlayerHeldObjects, Object[] PlayerPickUps, bool[] ThrowPressed) {
        if (ThrowPressed[player] && PlayerPickUps[player]) {
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

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] PlayerHeldObjects = new GameObject[] {null, Player1PickUp.objToPickUp, Player2PickUp.objToPickUp, Player3PickUp.objToPickUp, Player4PickUp.objToPickUp};

        Object[] PlayerPickUps = new Object[] {null, gameObject.GetComponent<Player1PickUp>(), gameObject.GetComponent<Player2PickUp>(), gameObject.GetComponent<Player3PickUp>(), gameObject.GetComponent<Player3PickUp>()};

        bool[] ThrowPressed = new bool[] {false, PlayerController.player1ThrowPressed, PlayerController.player2ThrowPressed, PlayerController.player3ThrowPressed, PlayerController.player4ThrowPressed};

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
    }
}
