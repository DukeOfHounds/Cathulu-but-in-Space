using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelDoorSystem : MonoBehaviour
{
    public GameObject Enteranceportal;
    public GameObject Exitceportal;
    public GameObject player;
    private List<GameObject> jewelList = new List<GameObject>();
    private GameObject[] jewels;
    public GameObject portalF;
    public GameObject portalB;
     
    private bool JewelsDestroyed = false;


    // Start is called before the first frame update
    void Start()
    {
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
            foreach (GameObject i in jewelList)//loops through jewels
            {
                if (!i.activeSelf)
                {
                    JewelsDestroyed = true;
                }
                if (i.activeSelf)
                {
                    JewelsDestroyed = false;
                }
            }
            if (JewelsDestroyed)// if all jewels are destroyed
            {
                portalF.SetActive(true);// spawn portal
                portalB.SetActive(true);// spawn portal
                Debug.Log("Boss is dead");
                // unfinished

                // boss death animation 
            }
        }
    }
    private void OnTriggerEnter()
    {
        Debug.Log("woosh woosh woosh");
        if (gameObject == Enteranceportal && player.gameObject.tag == "Player")
        {
            //collision.transform.position = Exitceportal.transform.position;

            player.transform.position = new Vector3(Exitceportal.transform.position.x, Exitceportal.transform.position.y, Exitceportal.transform.position.z);
        }
    }
}
