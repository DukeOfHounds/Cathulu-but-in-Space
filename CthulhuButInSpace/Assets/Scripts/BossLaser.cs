using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HealthAndDamage
{

    public class BossLaser : MonoBehaviour
    {

        public bool damageActive = false;

        // Update is called once per frame
        private void Awake()
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
        void Update()
        {
            if (damageActive)
            {
                this.gameObject.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().enabled = false;
            }
        }


        private void OnTriggerEnter(Collider collision)
        {
            if ((collision.gameObject.tag == "Player") && (damageActive == true))
            {
                collision.gameObject.GetComponent<HealthScript>().takeDamage(100f);
            }        
        }
    }
}