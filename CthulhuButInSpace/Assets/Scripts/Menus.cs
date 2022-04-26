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
        public Text Mission;
        public Canvas pauseScreen;
        public GameObject player;
        private GameObject gameManager;
        private GameObject keyDoor;

        private bool sP;
        // Start is called before the first frame update
        void Start()
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                gameManager = GameObject.Find("GameManager");
                keyDoor = GameObject.Find("Portal");
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
                    //if (player.GetComponent<Spaceship>().boosting)
                    //{
                    BoostingText.text = "Boost\n  Tank:\n    " + (int)player.GetComponent<Spaceship>().currentBoostAmount;
                    //}
                    //else
                    //{
                    //    BoostingText.text = "";
                    //}
                }
                if (keyDoor.GetComponent<JewelDoorSystem>().JewelsDestroyed && !keyDoor.GetComponent<JewelDoorSystem>().FoundPortal)
                {
                    Mission.text = "MISSION:\n  Find Portal\n      Home in\n         Asteroid";
                }
                if (keyDoor.GetComponent<JewelDoorSystem>().FoundPortal)
                {
                    Mission.text = "Good Work\n      Pilot,\n     Welcome\n         home ";
                    SceneManager.LoadScene("MainMenu");
                }

                if (Input.GetKeyDown(KeyCode.Escape) && sP)
                {
                    pauseScreen.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    sP = false;

                }
                else if (Input.GetKeyDown(KeyCode.Escape) && !sP)
                {
                    pauseScreen.gameObject.SetActive(true);
                    Time.timeScale = 0;
                    sP = true;
                }

                // Update 
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
        public void ResumeGame()
        {
            Time.timeScale = 1;
            pauseScreen.gameObject.SetActive(false);
            Cursor.visible = false;
        }
    }
}