using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public static int trashPutAway = 0;
    public AudioSource SFXCorrectTrash;
    public AudioSource SFXWrongTrash;
    public GameObject floatingPoints;

    private void setHasItem(Transform player)
    {
        switch(player.tag)
        {
            case "Player1":
                Debug.Log("Player 1 deposited trash");
                Player1PickUp.hasItem1 = false;
                //Debug.Log(Player1PickUp.hasItem);
                break;
            case "Player2":
                Debug.Log("Player 2 deposited trash");
                Player2PickUp.hasItem2 = false;
                //Debug.Log(Player1PickUp.hasItem);
                break;
            case "Player3":
                Debug.Log("Player 3 deposited trash");
                Player3PickUp.hasItem3 = false;
                //Debug.Log(Player1PickUp.hasItem);
                break;
            case "Player4":
                Debug.Log("Player 4 deposited trash");
                Player4PickUp.hasItem4 = false;
                //Debug.Log(Player1PickUp.hasItem);
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Plastic") && gameObject.tag == "PlasticBin")
        {
            Instantiate(floatingPoints, transform.position, Quaternion.identity);
            //Debug.Log("Collision with plastic trash bin");
            Destroy(other.gameObject);
            ScoreScript.scoreValue += 10;
            trashPutAway += 1;
            if(other.transform.parent != null)
                setHasItem(other.transform.parent);
            
            SFXCorrectTrash.Play();
        }
        // else if(gameObject.tag == "PlasticBin" && !other.gameObject.CompareTag("Plastic")  )
        // {
        //     Debug.Log("Wrong Trash");
        //     SFXWrongTrash.Play();
        // }
        else if(other.gameObject.CompareTag("GlassTrash") && gameObject.tag == "GlassBin")
        {
            Instantiate(floatingPoints, transform.position, Quaternion.identity);
            //Debug.Log("Collision with glass trash bin");
            Destroy(other.gameObject);
            ScoreScript.scoreValue += 10;
            trashPutAway += 1;
            if(other.transform.parent != null)
                setHasItem(other.transform.parent);
            
            SFXCorrectTrash.Play();
        }
        else if(other.gameObject.CompareTag("MetalTrash") && gameObject.tag == "MetalBin")
        {
            Instantiate(floatingPoints, transform.position, Quaternion.identity);
            //Debug.Log("Collision with metal trash bin");
            Destroy(other.gameObject);
            ScoreScript.scoreValue += 10;
            trashPutAway += 1;
            if(other.transform.parent != null)
                setHasItem(other.transform.parent);
            
            SFXCorrectTrash.Play();
        }
        else if(other.gameObject.CompareTag("OrganicTrash") && gameObject.tag == "OrganicTrashBin")
        {
            Instantiate(floatingPoints, transform.position, Quaternion.identity);
            //Debug.Log("Collision with organic trash bin");
            Destroy(other.gameObject);
            ScoreScript.scoreValue += 10;
            trashPutAway += 1;
            if(other.transform.parent != null)
                setHasItem(other.transform.parent);
            
            SFXCorrectTrash.Play();
        } else if(other.gameObject.layer != 6 && (trashPutAway != 0))
        {
            Debug.Log("Wrong Trash");
            SFXWrongTrash.Play();
        }
        
    }
}
