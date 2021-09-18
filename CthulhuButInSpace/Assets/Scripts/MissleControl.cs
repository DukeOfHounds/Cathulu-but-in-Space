using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MissleControl : MonoBehaviour
{
    public Transform LaunchPoint;
    public Camera cam;
    public GameObject projectile;
    public Rigidbody spaceship;
    public float projectileSpeed = 1;
    private List<GameObject> MissilesFired = new List<GameObject>();
    private Vector3 destination;
    private bool isFiring;

    [Header("=== Missle Settings ===")]
    [SerializeField]
    private float speed = 100000;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LaunchMissile()
    {
        //creates a projectile at LuanchPoint, with a rotaition facing ship forward. 
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);

        InstantiateProjectile(LaunchPoint);
    }

    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
    }

    IEnumerator LaunchCheck()
    {
        isFiring = true;
        LaunchMissile();
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
    
