using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    // Trash Spawner for our Trash Objects
    // public lets it show up in the Unity Inspector
    public GameObject PlasticTrash;
    public GameObject GlassTrash;
    public float repeatTime = 5f;
    public const float COLUMNS = 23f; // 5 - 25
    public const float ROWS = 21.15f; // -23 - 23
    public float trashCount = 0;

    private System.Random random = new System.Random();

    void Start() {
        InvokeRepeating("Spawn", 2f, repeatTime);
    }
        
    void Spawn() {
        float y = 10f;
        float RandomRow = (float)random.Next(-(int)ROWS,(int)ROWS); // Generate a Random X value
        float RandomCol = (float)random.Next(5, (int)COLUMNS); // Generate a Random Z value

        GameObject newTrash;
        int trashType = random.Next(0, 2);  // Create a random number between 0 and 1
        Debug.Log("Trash type spawned " + trashType);

        if (trashType == 0)
        {
            newTrash = GameObject.Instantiate(PlasticTrash);
            newTrash.transform.position = new Vector3(RandomRow, y, RandomCol);
        }

        else if (trashType == 1)
        {
            newTrash = GameObject.Instantiate(GlassTrash);
            newTrash.transform.position = new Vector3(RandomRow, y, RandomCol);
        }

        trashCount += 1;   
    }

}
