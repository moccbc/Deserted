using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public static int trashPutAway = 0;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Plastic") && gameObject.tag == "PlasticBin")
        {
            //Debug.Log("Collision with plastic trash bin");
            Destroy(other.gameObject);
            ScoreScript.scoreValue += 10;
            trashPutAway += 1;
            PickUp.hasItem = false;
        }
        else if(other.gameObject.CompareTag("GlassTrash") && gameObject.tag == "GlassBin")
        {
            //Debug.Log("Collision with glass trash bin");
            Destroy(other.gameObject);
            ScoreScript.scoreValue += 10;
            trashPutAway += 1;
            PickUp.hasItem = false;
        }
        else if(other.gameObject.CompareTag("MetalTrash") && gameObject.tag == "MetalBin")
        {
            //Debug.Log("Collision with metal trash bin");
            Destroy(other.gameObject);
            ScoreScript.scoreValue += 10;
            trashPutAway += 1;
            PickUp.hasItem = false;
        }
        else if(other.gameObject.CompareTag("OrganicTrash") && gameObject.tag == "OrganicTrashBin")
        {
            //Debug.Log("Collision with organic trash bin");
            Destroy(other.gameObject);
            ScoreScript.scoreValue += 10;
            trashPutAway += 1;
            PickUp.hasItem = false;
        }
    }
}
