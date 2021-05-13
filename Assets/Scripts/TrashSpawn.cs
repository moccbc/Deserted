using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    // Trash Spawner for our Trash Objects
    // public lets it show up in the Unity Inspector
    public GameObject PlasticTrash;
    public GameObject GlassTrash;
    public GameObject MetalTrash;
    public GameObject OrganicTrash;
    public GameObject SprintPowerUp;

    public float repeatTime = 5f;
    public float powerUpSpawnInterval = 15f;    // Spawn power-ups every 15 seconds
    public const float COLUMNS = 23f; // 5 - 25
    public const float ROWS = 21.15f; // -23 - 23
    public float trashCount = 0;

    private System.Random random = new System.Random();

    void Start() {
        InvokeRepeating("Spawn", 2f, repeatTime);
        InvokeRepeating("SpawnPowerUp", 5f, powerUpSpawnInterval);
    }

    public void SpawnPowerUp()
    {
        GameObject newPowerUp;
        newPowerUp = GameObject.Instantiate(SprintPowerUp);
        newPowerUp.transform.position = new Vector3(0.41f, 10f, 8.72f);   // Try to spawn in the center of the arena
        //Debug.Log("SPAWN POWER UP");
    }

    void Spawn() {
        float y = 10f;
        float RandomRow = (float)random.Next(-(int)ROWS,(int)ROWS); // Generate a Random X value
        float RandomCol = (float)random.Next(5, (int)COLUMNS); // Generate a Random Z value

        GameObject newTrash;
        int trashType = random.Next(0, 4);  // Create a random number between 0 and 3 inclusive 

        if (trashType == 0)
        {
            newTrash = GameObject.Instantiate(PlasticTrash);
            newTrash.transform.position = new Vector3(RandomRow, y, RandomCol);
            //Debug.Log("Plastic trash spawned " ); 
        }

        else if (trashType == 1)
        {
            newTrash = GameObject.Instantiate(GlassTrash);
            newTrash.transform.position = new Vector3(RandomRow, y, RandomCol);
            //Debug.Log("Glass trash spawned " ); 
        }

        else if (trashType == 2)
        {
            newTrash = GameObject.Instantiate(MetalTrash);
            newTrash.transform.position = new Vector3(RandomRow, y, RandomCol);
            //Debug.Log("Metal trash spawned " ); 
        }

        else if (trashType == 3)
        {
            newTrash = GameObject.Instantiate(OrganicTrash);
            newTrash.transform.position = new Vector3(RandomRow, y, RandomCol);
            //Debug.Log("Organic trash spawned " ); 
        }

        trashCount += 1;   
    }
}
