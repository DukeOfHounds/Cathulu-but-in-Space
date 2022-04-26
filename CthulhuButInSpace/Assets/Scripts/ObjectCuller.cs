using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCuller : MonoBehaviour
{
    private GameObject culler = null;
    public float killDist = 1000f;
    private CullingScript cS = null;


    private void Awake()

    {
        culler = GameObject.Find("Culler");
        cS = culler.GetComponent<CullingScript>();
    }

    private void Start()
    {
        // Use a public method to add to the list, rather than making it public:
        cS.Register(new Cull(this.gameObject, this.transform.position, this.killDist));
    }
}