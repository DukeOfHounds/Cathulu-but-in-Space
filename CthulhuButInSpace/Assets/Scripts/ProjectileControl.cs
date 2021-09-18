using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ProjectileControl : MonoBehaviour
{
   
    public Camera cam;
    public Rigidbody spaceship;

    public Transform laserLaunchPoint;
    public GameObject laser;
    public float laserSpeed = 1; 

    public Transform missileLaunchPoint;
    public GameObject missile;
    public float missileSpeed = 1;

    private List<GameObject> ProjectilesFired = new List<GameObject>();
    private Vector3 destination;
    private bool isFiring;

    [Header("=== Missle Settings ===")]
    [SerializeField]
    private float speed = 100000;

    public void FireLaser()
    {
        //creates a projectile at LuanchPoint, with a rotaition facing ship forward. 
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);

        InstantiateProjectile(laserLaunchPoint);
    }

    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(laser, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * laserSpeed;
    }

    IEnumerator LaunchCheck()
    {
        isFiring = true;
        FireLaser();
        yield return new WaitForSeconds(.2f);
        isFiring = false;

    }




    #region Input Methods
    public void OnLaunchMissile(InputAction.CallbackContext context)
        {
            if (isFiring == false)
            {
                StartCoroutine(LaunchCheck());
            }
        
        }
    #endregion

}
    
