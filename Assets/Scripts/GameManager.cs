using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField]
    private TrashSpawn TrashSpawner;
    //[SerializeField]
    //private TrashBin trashBin;
    //public ScoreScript scoreScript;
    public TextMeshProUGUI finalScore;
    public bool gameOver;

    public void EndGame()
    {
        gameOver = true;
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);
        TrashSpawner.isSpawning = false;
        finalScore.text = "Final Score: " + ScoreScript.scoreValue;
        ScoreScript.scoreValue = 0;
        TrashSpawner.trashCount = 0;
        TrashBin.trashPutAway = 0;
    }
}
