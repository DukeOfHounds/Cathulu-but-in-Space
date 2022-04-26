using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HealthAndDamage
{
    public class Collisions : MonoBehaviour
    {


       private bool startOfGame = true;

        private void Start()
        {
            StartCoroutine(startGameCheck());
        }
        IEnumerator startGameCheck()
        {
          
            yield return new WaitForSeconds(8f);
            startOfGame = false;

        }
        private void OnCollisionEnter(Collision collision)
        {
             if(startOfGame == false) 
            { 
                float damage;
                if ((gameObject.tag == "Asteroid" || gameObject.tag == "Scrap" || gameObject.tag == "Jewel") && (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Scrap" || collision.gameObject.tag == "Jewel"))
                {
                    
                    damage = gameObject.GetComponent<Rigidbody>().mass * (gameObject.GetComponent<Rigidbody>().velocity.magnitude + collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude);
                    collision.gameObject.GetComponent<HealthScript>().takeDamage(damage);
                    damage = collision.gameObject.GetComponent<Rigidbody>().mass;
                    gameObject.GetComponent<HealthScript>().takeDamage(damage);
                }


                if (gameObject.tag == "Player" && (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Scrap") )// if the player colides with a debries
                {
                    damage = collision.gameObject.GetComponent<Rigidbody>().mass;
                    gameObject.GetComponent<HealthScript>().takeDamage(damage);
                }
            }
        }
    }
}
