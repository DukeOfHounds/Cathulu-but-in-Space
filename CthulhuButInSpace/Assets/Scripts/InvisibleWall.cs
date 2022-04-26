using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
