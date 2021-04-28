using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private static bool hasItem;

    // Start is called before the first frame update
    void Start()
    {
        hasItem = PickUp.hasItem;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Trash"))
        {
            Destroy(other.gameObject);
            ScoreScript.scoreValue += 10;
            PickUp.hasItem = false;
        }
    }
}
