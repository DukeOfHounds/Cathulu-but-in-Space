using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace HealthAndDamage
{

    
    public class GameManager : MonoBehaviour
    {
        GameObject player;
         void start()
        {
            player = PlayerManager.instance.player;
        }
        [Header("=== Menue Toggles ===")]
        [SerializeField]
        public bool gameOver = false; // should the gameOver screen show bool

        [Header("=== Menue Toggles ===")]
        [SerializeField]
        private float timeDelay = 2f;// pause in time before a UI is displayed

        public GameObject gameOverUI;// game over UI
        public bool tutorial = true;

        public AudioClip AS1;
        public AudioClip AS2;
        
        private void Update()// for testing purposes. 
        {
            if (gameOver == true)
            {
                DisplayGameOver();
            }
            if (gameOver == false)
            {

                CloseGameOver();

            }

        }
        public void GameOver()// public method displaying UI that can be referenced by classes
        {
            if (gameOver == false)
            {
                gameOver = true;
                Time.timeScale = 0;
                Invoke("DisplayGameOver", timeDelay); //Opens UI

            }

        }
        public void GameNotOver()// public method Closing UI that can be referenced by classes
        {
            if (gameOver == true)
            {
                gameOver = false;
                Invoke("CloseGameOver", timeDelay); // closes UI

            }

        }
        private void DisplayGameOver()// sets Game Over screen to visible 
        {
            Cursor.visible = true;
            gameOverUI.SetActive(true);

        }
        private void CloseGameOver()// sets Game Over screen to visible 
        {
            Cursor.visible = false;
            gameOverUI.SetActive(false);

        }

    }
}
