using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HealthAndDamage
{
    public class HealthScript : MonoBehaviour
    {

        public int health = 1;
        public GameObject FracturedMesh;

        // Start is called before the first frame update
        public void takeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Instantiate(FracturedMesh, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
