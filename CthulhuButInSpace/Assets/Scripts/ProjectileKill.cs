using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileKill : MonoBehaviour
{
    public GameObject projectile;
    public float timeToKill;

    // Update is called once per frame
    void Update()
    {
        Destroy(projectile, timeToKill);  
    }
}
