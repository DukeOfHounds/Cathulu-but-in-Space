using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CullingScript : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    public List<Cull> cullingObjs;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        this.gameObject.SetActive(false);
        cullingObjs = new List<Cull>();
        StartCoroutine(IsActive());
    }

    IEnumerator IsActive()
    {
        List<Cull> removeList = new List<Cull>();
        if (cullingObjs.Count > 0)
        {
            foreach (Cull i in cullingObjs)
            {
                if (Vector3.Distance(target.position, i.objPos) > 8000)
                {
                    if (i.item == null)
                    {
                        removeList.Add(i);
                    }
                    else
                    {
                        i.item.SetActive(false);
                    }
                }
                else
                {
                    if (i.item == null)
                    {
                        removeList.Add(i);
                    }
                    else
                    {
                        i.item.SetActive(true);
                    }
                }
            }

        }
        yield return new WaitForSeconds(0.01f);

        if (removeList.Count > 0)
        {
            foreach (Cull i in removeList)
            {
                cullingObjs.Remove(i);
            }
        }
        yield return new WaitForSeconds(0.01f);

        StartCoroutine(IsActive());
    }



    // Update is called once per frame
    void Update()
    {
        float targetDistance = Vector3.Distance(target.position, transform.position);

            if (targetDistance <= 10000f)
            this.gameObject.SetActive(true);
            else
            this.gameObject.SetActive(false);
    }
    
}

public class Cull
{
    public GameObject item;
    public Vector3 objPos;
}