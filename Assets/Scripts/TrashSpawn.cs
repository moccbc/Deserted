using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    // Trash Spawner for our Trash Objects
    // public lets it show up in the Unity Inspector

    // The different types of trash to be spawned 
    public GameObject PlasticTrash;
    public GameObject GlassTrash;
    public GameObject MetalTrash;
    public GameObject OrganicTrash;

    // The different power ups to be spawned
    public GameObject SprintPowerUp;
    public GameObject DestroyTrashPowerUp;

    public float repeatTime = 5f;
    public float powerUpSpawnInterval = 15f;    // Spawn power-ups every 15 seconds
    public const float COLUMNS = 23f;           // 5 - 25
    public const float ROWS = 21.15f;           // -23 - 23
    public float trashCount = 0;

    private System.Random random = new System.Random();

    void Start() {
        InvokeRepeating("Spawn", 2f, repeatTime);

        // Call the SpawnPowerUp function every 15 secs after the first 5 seconds have elapsed
        InvokeRepeating("SpawnPowerUp", 5f, powerUpSpawnInterval);  
    }

    // Spawns a random power up every 15 secs (adjustable) starting after the first 5 seconds of the game have elapsed
    public void SpawnPowerUp()
    {
        GameObject newPowerUp;
        int powerUpType = random.Next(1, 3);    // Create a random number between 1 and 2, inclusive. Represents the power up type
        
        switch(powerUpType)
        {
            // Spawn a Sprint power up at a random location on the map
            case 1:
                newPowerUp = GameObject.Instantiate(SprintPowerUp);
                newPowerUp.transform.position = new Vector3(0.41f, 10f, 8.72f);   // Try to spawn in the center of the arena
                break;

            // Spawn a Destroy Trash power up at a random location on the map
            case 2:
                newPowerUp = GameObject.Instantiate(DestroyTrashPowerUp);
                newPowerUp.transform.position = new Vector3(0.41f, 10f, 8.72f);   // Try to spawn in the center of the arena
                break;

            default:
                break;
        }

        //Debug.Log("SPAWN POWER UP");
    }

    void Spawn() {
        float y = 10f;
        float RandomRow = (float)random.Next(-(int)ROWS,(int)ROWS);     // Generate a Random X value
        float RandomCol = (float)random.Next(5, (int)COLUMNS);          // Generate a Random Z value

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
