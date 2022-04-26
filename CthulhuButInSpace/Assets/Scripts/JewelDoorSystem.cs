using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelDoorSystem : MonoBehaviour
{
    public GameObject Enteranceportal;
    public GameObject Exitceportal;
    public GameObject player;
    private GameObject Boss;
    public Animator animator;


    private List<GameObject> jewelList = new List<GameObject>();
    private GameObject[] jewels;
    public GameObject portalF;
    public GameObject portalB;

     
    public bool JewelsDestroyed = false;
    public bool FoundPortal = false;
    private List<bool> destroyedJewels = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.Find("Cthulhu_Rig");
        FoundPortal = false;
        portalF.SetActive(false);
        portalB.SetActive(false);
        //add jewels to array
        jewels = GameObject.FindGameObjectsWithTag("Jewel"); // fills array
        foreach (GameObject i in jewels)// fills list
        {
            jewelList.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!JewelsDestroyed)
        {
            bool destroyed = true;
            foreach (GameObject i in jewelList)//loops through jewels
            {
                if (!i.activeSelf)
                {
                    destroyedJewels.Add(false);
                }
                if (i.activeSelf)
                {
                    destroyedJewels.Add(true);
                }
            }
            foreach (bool i in destroyedJewels)// if all jewels  are inactive destryoed stays true
            {
                if (i == true)
                {
                     destroyed = false;
                }
            }
            destroyedJewels.Clear();// empties list of old data
            if (destroyed)
            {
                JewelsDestroyed = true;
            }
            if (JewelsDestroyed)// if all jewels are destroyed
            {
                //animator.SetBool("Death", true);
                Boss.gameObject.GetComponent<BossController>().Death();
                portalF.SetActive(true);// spawn portal
                portalB.SetActive(true);// spawn portal
                Debug.Log("Boss is dead");
                // unfinished

                // boss death animation 
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (gameObject == Enteranceportal && collision.gameObject.tag == "Player")
        {
            //collision.transform.position = Exitceportal.transform.position;
            FoundPortal = true;
            player.transform.position = new Vector3(Exitceportal.transform.position.x, Exitceportal.transform.position.y, Exitceportal.transform.position.z);
        }
    }
}
