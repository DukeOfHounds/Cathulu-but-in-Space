using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFragments : MonoBehaviour
{
    public float minTime = 5f;
    public float maxTime = 10f;
    public bool isMaster;
    private float killTimer = 0.0f;
    IEnumerator KillFragment()
    {
        yield return new WaitForSeconds(killTimer);
        if (isMaster)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        killTimer = Random.Range(minTime, maxTime);
        StartCoroutine(KillFragment());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
