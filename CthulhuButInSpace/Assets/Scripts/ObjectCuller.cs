using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCuller : MonoBehaviour
{

    public GameObject listObj;
    private CullingScript cS;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("AddToList");
    }

    // Update is called once per frame

    IEnumerator AddToList()
    {
        
        yield return new WaitForSeconds(6f);
        listObj = GameObject.Find("Culler");
        cS = listObj.GetComponent<CullingScript>();
        cS.cullingObjs.Add(new Cull { item = this.gameObject, objPos = transform.position });
    }
}
