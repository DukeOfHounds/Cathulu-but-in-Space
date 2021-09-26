using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HealthAndDamage
{
    public class ProjectileKill : MonoBehaviour
    {
        public GameObject projectile;
        public float timeToKill = 2;
        public GameObject killParticle;
        public int Damage = 2;

        // Update is called once per frame
        void Update()
        {
            Destroy(projectile, timeToKill);
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Asteroid")
            {
                Instantiate(killParticle, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
                collision.gameObject.GetComponent<HealthScript>().takeDamage(Damage);         
                if (projectile.gameObject.tag == "Missile")
                {
                    Destroy(projectile);
                }

            }
        }
    }
}
