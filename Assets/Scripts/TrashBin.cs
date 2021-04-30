using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private static bool hasItem;
    public TrashBar trashBar;

    public static int trashPutAway = 0;
    // Start is called before the first frame update
    void Start()
    {
        hasItem = PickUp.hasItem;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasItem)
            Debug.Log("Player is holding trash");
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Trash"))
        {
            Debug.Log("Collision with trash bin");
            Destroy(other.gameObject);
            ScoreScript.scoreValue += 10;
            trashPutAway += 1;
            PickUp.hasItem = false;
        }
    }
}
