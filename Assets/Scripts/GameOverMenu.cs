using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private TrashSpawn TrashSpawner;
    [SerializeField]
    private GameManager gameManager;
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TrashSpawner.isSpawning = true;
        gameManager.gameOver = false;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
