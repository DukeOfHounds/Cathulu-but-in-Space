using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HealthAndDamage
{
    public class HealthScript : MonoBehaviour
    {

        public float health = 1;
        public GameObject FracturedMesh;

        // Start is called before the first frame update
        public void takeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                if (gameObject.tag == "Asteroid")
                {
                    Debug.Log("Boom");
                    Instantiate(FracturedMesh, transform.position, transform.rotation);
                    Destroy(gameObject);

                }
                else if(gameObject.tag == "Scrap")
                {
                    Destroy(gameObject);
                }
                else if(gameObject.tag == "Jewel")
                {
                    Destroy(gameObject);
                }
                else if(gameObject.tag == "Player")
                {
                    // not finished
                }
            }
        }
        
    }
}
