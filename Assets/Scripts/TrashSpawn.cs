using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    // Trash Spawner for our Trash Objects
    // public lets it show up in the Unity Inspector
    public GameObject Trash_Block;
    public float repeatTime = 5f;
    public const float COLUMNS = 23f; // 5 - 25
    public const float ROWS = 21.15f; // -23 - 23
    public float trashCount = 0;
    public bool isSpawning;

    private System.Random random = new System.Random();

    void Start() {
        InvokeRepeating("Spawn", 2f, repeatTime);
    }
        
    void Spawn() {
        if (isSpawning == true)
        {
            float y = 10f;
            float RandomRow = (float)random.Next(-(int)ROWS, (int)ROWS); // Generate a Random X value
            float RandomCol = (float)random.Next(5, (int)COLUMNS); // Generate a Random Z value
            GameObject newTrash = GameObject.Instantiate(Trash_Block);
            newTrash.transform.position = new Vector3(RandomRow, y, RandomCol);
            trashCount += 1;
        }
        else
        {
            return;
        }
    }

}
