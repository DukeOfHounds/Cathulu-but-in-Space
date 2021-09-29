using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelDoorSystem : MonoBehaviour
{
    private List<GameObject> jewelList = new List<GameObject>();
    private GameObject[] jewels;



    // Start is called before the first frame update
    void Start()
    {
        //add jewels to array
       jewels = GameObject.FindGameObjectsWithTag("Jewel");
        foreach (GameObject i in jewels)
        {
            jewelList.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //loops through jewels
            // if all jewels are destroyed
                // spawn portal
                // boss death animation 

    }
}
