using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("=== Menue Toggles ===")]
    [SerializeField]
    private bool gameOver = false;

    [Header("=== Menue Toggles ===")]
    [SerializeField]
    private float timeDelay = 2f;

    public GameObject gameOverUI;
    public void GameOver()
    {
        if (gameOver == false)
        {
            gameOver = true;
            Debug.Log("GAME OVER");
            Invoke("DisplayGameOver", timeDelay);
           
        }
        
    }
    private void DisplayGameOver()// sets Game Over screen to visible 
    {
        gameOverUI.SetActive(true);
    }
    public void Restart()// Reloads current Scene
    {
        gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
