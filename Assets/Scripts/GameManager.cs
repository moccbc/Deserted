using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField]
    private TrashSpawn TrashSpawner;
    [SerializeField]
    private GameObject player1;
    public PlayerController player1movement;
    [SerializeField]
    private GameObject player2;
    public PlayerController player2movement;
    [SerializeField]
    private GameObject player3;
    public PlayerController player3movement;
    [SerializeField]
    private GameObject player4;
    public PlayerController player4movement;
    //[SerializeField]
    //private TrashBin trashBin;
    //public ScoreScript scoreScript;
    public TextMeshProUGUI finalScore;
    public bool gameOver;

    public void EndGame()
    {
        player1 = GameObject.Find("Player1(Clone)");
        player1movement = player1.GetComponent<PlayerController>();
        player1movement.canMove = false;

        player2 = GameObject.Find("Player2(Clone)");
        player2movement = player2.GetComponent<PlayerController>();
        player2movement.canMove = false;

        player3 = GameObject.Find("Player3(Clone)");
        player3movement = player3.GetComponent<PlayerController>();
        player3movement.canMove = false;

        player4 = GameObject.Find("Player4(Clone)");
        player4movement = player4.GetComponent<PlayerController>();
        player4movement.canMove = false;

        gameOver = true;
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);
        TrashSpawner.isSpawning = false;
        //playerController.canMove = false;
        finalScore.text = "Final Score: " + ScoreScript.scoreValue;
        ScoreScript.scoreValue = 0;
        TrashSpawner.trashCount = 0;
        TrashBin.trashPutAway = 0;
    }
}
