using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HealthAndDamage
{
    public class ProjectileControl : MonoBehaviour
    {

        public Camera cam;
        public Rigidbody spaceship;

        public Transform laserLaunchPoint1;
        public Transform laserLaunchPoint2;
        public GameObject laser;
        public float laserSpeed = 1;

        public Transform missileLaunchPoint1;
        public Transform missileLaunchPoint2;
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

            InstantiateProjectile(laserLaunchPoint1, laser);
            InstantiateProjectile(laserLaunchPoint2, laser);
        }
        public void FireMissile()
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                destination = hit.point;
            else
                destination = ray.GetPoint(1000);

            InstantiateProjectile(missileLaunchPoint1, missile);
            InstantiateProjectile(missileLaunchPoint2, missile);
        }


        void InstantiateProjectile(Transform firePoint, GameObject projectile)
        {
            var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
            projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * laserSpeed + spaceship.velocity;
        }


        IEnumerator LaunchCheckLaser()
        {
            isFiring = true;
            FireLaser();
            yield return new WaitForSeconds(.2f);
            isFiring = false;

        }
        IEnumerator LaunchCheckMissile()
        {
            isFiring = true;
            FireMissile();
            yield return new WaitForSeconds(.2f);
            isFiring = false;

        }



        #region Input Methods
        public void OnLaunchLaser(InputAction.CallbackContext context)
        {
            if (isFiring == false)
            {
                StartCoroutine(LaunchCheckLaser());
            }

        }
        public void OnLaunchMissile(InputAction.CallbackContext context)
        {
            if (isFiring == false)
            {
                StartCoroutine(LaunchCheckMissile());
            }

        }
        #endregion

    }

}