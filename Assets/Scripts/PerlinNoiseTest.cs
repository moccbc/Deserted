using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PerlinNoiseTest : MonoBehaviour
{
   public GameObject CurrBlockType; 
   public float amplitude = 2.5f;
   public float frequency = 5f;
   public const float COLUMNS = 20f;
   public const float ROWS = 15f;

   private System.Random CornerRand = new System.Random();

   void Start() {
       generateTerrain();
   }

   int ChooseCorner() {
       return CornerRand.Next(1,5); // Chooses a number between 1 and 4
   }

    void generateTerrain() {
      float perlinGridX = 4f;
      float perlinGridZ = 4f;
      float ground_y = 0.5f;
      int PerchPoint = ChooseCorner() - 1; // Gets a random value from 0 - 3 (to choose the Corner)
      int WaterPoint = ChooseCorner() - 1; // Gets a random value from 0 - 3 (to choose the Corner)

      while(PerchPoint == WaterPoint) { // Get a unique value for both PerchPoint and Water Point
          WaterPoint = ChooseCorner() - 1;
      }
    for(float x = 0f; x < COLUMNS; x+= 0.5f) {
        for(float z = 0f; z < ROWS; z+= 0.5f) {
            for(float y = 0f; y < ground_y; y+= 0.5f) {
                  GameObject newBlock = GameObject.Instantiate(CurrBlockType);
                  newBlock.transform.position = new Vector3(x,y,z);
              }                             
          } 
    }
    switch(PerchPoint) {
        case 0: // 1st Corner
            addPerlinNoise(0,0, perlinGridX, perlinGridZ, ground_y, false);  
            break;
        case 1: // 2nd Corner
            addPerlinNoise(ROWS - perlinGridX, 0, ROWS, perlinGridZ, ground_y, false);
            break;
        case 2: // 3rd Corner
            addPerlinNoise(0, COLUMNS - perlinGridZ, perlinGridX, COLUMNS, ground_y, false);
            break;
        case 3: // 4th Corner
            addPerlinNoise(ROWS - perlinGridX , COLUMNS - perlinGridZ, ROWS, COLUMNS, ground_y, false);
            break;
      } 
    }
   /*  Z
    X [0  1  2  3  4  5  6  7  8  9]
      [1  0  0  0  0  0  0  0  0  0]
      [2  0  0  0  0  0  0  0  0  0]
      [3  0  0  0  0  0  0  0  0  0] 
      [4  0  0  0  0  0  0  0  0  0]
      [5  0  0  0  0  0  0  0  0  0]
      [6  0  0  0  0  0  0  0  0  0]
      [7  0  0  0  0  0  0  0  0  0]
      [8  0  0  0  0  0  0  0  0  0]
      [9  0  0  0  0  0  0  0  0  0]
   */
   void addPerlinNoise(float xPos, float zPos, float cols, float rows, float ground_y, bool indent)
   {
       if(!indent) 
       {
            for(float x = xPos; x < cols; x += 0.5f) {
                for(float z = zPos; z < rows; z += 0.5f) {
                float y = Mathf.PerlinNoise(x/frequency, z/frequency) * amplitude;
                    for(float y_fill = ground_y; y_fill < y; y_fill += 0.5f) {
                    GameObject newBlock = GameObject.Instantiate(CurrBlockType);
                    newBlock.transform.position = new Vector3(x,y_fill,z); 
                    }   
                }
            }
       } 
       else
       {
           for(float x = xPos; x < cols; x += 0.5f) {
                for(float z = zPos; z < rows; z += 0.5f) {
                float y = Mathf.PerlinNoise(x/frequency, z/frequency) * amplitude;
                y = y * -1;
                    for(float y_fill = ground_y; y_fill > y; y_fill -= 0.5f) {
                    GameObject newBlock = GameObject.Instantiate(CurrBlockType);
                    newBlock.transform.position = new Vector3(x,y_fill,z); 
                    }   
                }
            }
       }
       
   }    
}
