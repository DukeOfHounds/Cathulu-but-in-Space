using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HealthAndDamage
{

    public class BossLaser : MonoBehaviour
    {

        public bool damageActive = false;
        public GameObject r1;
        public GameObject r2;
        private GameObject target;
        private bool triggerActive = false;



        // Update is called once per frame
        private void Awake()
        {
            r1 = GameObject.Find("r1");
            r2 = GameObject.Find("r2");

        }

        private void Start()
        {
            r1 = GameObject.Find("r1");
            r2 = GameObject.Find("r2");
            r1.GetComponent<Renderer>().enabled = false;
            r2.GetComponent<Renderer>().enabled = false;
        }
        void Update()
        {

            if (damageActive)
            {
                r1.GetComponent<Renderer>().enabled = true;
                r2.GetComponent<Renderer>().enabled = true;
                if(triggerActive)
                {
                    target.GetComponent<HealthScript>().takeDamage(5f);
                }
            }
            
            else
            {
                r1.GetComponent<Renderer>().enabled = false;
                r2.GetComponent<Renderer>().enabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                target = other.gameObject;

                if (damageActive)
                {
                    triggerActive = true;
                }

            }
                
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                target = other.gameObject;

                if (damageActive)
                {
                    triggerActive = true;
                }

            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {


                if (damageActive)
                {
                    triggerActive = false;
                }

            }
        }
    }
}