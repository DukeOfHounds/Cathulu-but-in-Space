using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace HealthAndDamage
{
    public class Menus : MonoBehaviour
    {
        public Text healthText; 
        public Text speedText;
        public Text BoostingText;
        public GameObject player;
        private GameObject gameManager;
        // Start is called before the first frame update
        void Start()
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                gameManager = GameObject.Find("GameManager");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                if (!gameManager.GetComponent<GameManager>().gameOver)
                {
                    healthText.text = "Health: " + (int)player.GetComponent<HealthScript>().health;
                    speedText.text = "Speed: " + (int)player.GetComponent<Rigidbody>().velocity.magnitude;
                    if (player.GetComponent<Spaceship>().boosting)
                    {
                        BoostingText.text = "BOOSTING";
                    }
                    else
                    {
                        BoostingText.text = "";
                    }
                }
            }
        }
        public void Restart()
        {
            Cursor.visible = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Reloads current Scene
            Time.timeScale = 1;

        }
        public void PlayGame()
        {
            SceneManager.LoadScene("Level1");
        }
        public void QuitGame()
        {
            Application.Quit();
        }

    }
}