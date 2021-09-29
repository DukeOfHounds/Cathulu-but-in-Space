using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFragments : MonoBehaviour
{
    private float killTimer = 0.0f;
    IEnumerator KillFragment()
    {
        yield return new WaitForSeconds(killTimer);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        killTimer = Random.Range(10.0f, 30.0f);
        StartCoroutine(KillFragment());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
