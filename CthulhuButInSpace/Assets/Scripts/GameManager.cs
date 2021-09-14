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

    private void Update()
    {
        if (gameOver == true)
        {
            Invoke("DisplayGameOver", timeDelay);
        }
        if (gameOver == false)
        {

            Invoke("CloseGameOver", timeDelay);

        }

    }
    public void GameOver()
    {
        if (gameOver == false)
        {
            gameOver = true;
            Debug.Log("GAME OVER");
           
        }
        
    }
    private void DisplayGameOver()// sets Game Over screen to visible 
    {
        Cursor.visible = true;
        gameOverUI.SetActive(true);
        
    }
    private void CloseGameOver()// sets Game Over screen to visible 
    {

        gameOverUI.SetActive(false);

    }
    public void Restart()// Reloads current Scene
    {
        gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
