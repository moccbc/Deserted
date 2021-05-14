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
        //hasItem = PlayerController.hasItem;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.hasItem)
            Debug.Log("Player is holding trash");
    }

    private void OnCollisionEnter(Collision item)
    {
        if(item.gameObject.CompareTag("Trash"))
        {
            Debug.Log("Collision with trash bin");
            Destroy(item.gameObject);
            ScoreScript.scoreValue += 10;
            trashPutAway += 1;
            PlayerController.hasItem = false;
        }
    }
}
