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

        private bool weaponHolstered = true;
        private float weaponHolserCD = 0.0f;
        private float weaponHolsterTimer = 3.0f;

        public Animator laserL;
        public Animator laserR;
        public ParticleSystem laserLParticle;
        public ParticleSystem laserRParticle;

        public Animator missileL;
        public Animator missileR;

        [Header("=== Laser Settings ===")]
        public Transform laserLaunchPoint1;
        public Transform laserLaunchPoint2;
        public GameObject laser;
        public float laserSpeed = 1;

        [Header("=== Missle Settings ===")]
        public Transform missileLaunchPoint1;
        public Transform missileLaunchPoint2;
        public GameObject missile;
        public float missileSpeed = 1;

        [Header("=== Shock Wave Settings ===")]
        public Transform shockWaveLaunchPoint1;
        public Transform shockWaveLaunchPoint2;
        public GameObject shockWave;
        public float shockWaveSpeed = 1;

        private List<GameObject> ProjectilesFired = new List<GameObject>();
        private Vector3 destination;
        private bool isFiring;

        
     

        public void FireLaser()
        {
            if (weaponHolstered == false)
            {
                laserL.SetBool("Fire", true);
                laserR.SetBool("Fire", true);
            }

            laserL.SetBool("inCombat", true);
            laserR.SetBool("inCombat", true);
            missileL.SetBool("inCombat", true);
            missileR.SetBool("inCombat", true);

            weaponHolstered = false;
            weaponHolserCD = weaponHolsterTimer;

            //creates a projectile at LuanchPoint, with a rotaition facing ship forward.            
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                destination = hit.point;
            else
                destination = ray.GetPoint(1000);

            InstantiateProjectile(laserLaunchPoint1, laser, laserSpeed);
            InstantiateProjectile(laserLaunchPoint2, laser, laserSpeed);
            
            laserLParticle.Play();
            laserRParticle.Play();

        }
        public void FireShockWave()
        {
            //creates a projectile at LuanchPoint, with a rotaition facing ship forward. 
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                destination = hit.point;
            else
                destination = ray.GetPoint(1000);

            InstantiateProjectile(laserLaunchPoint1, shockWave,shockWaveSpeed);
            InstantiateProjectile(laserLaunchPoint2, shockWave, shockWaveSpeed);
        }
        public void FireMissile()
        {
            if (weaponHolstered == false)
            {
                missileL.SetBool("Fire", true);
                missileR.SetBool("Fire", true);
            }
            
            laserL.SetBool("inCombat", true);
            laserR.SetBool("inCombat", true);
            missileL.SetBool("inCombat", true);
            missileR.SetBool("inCombat", true);

            weaponHolstered = false;
            weaponHolserCD = weaponHolsterTimer;
            
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                destination = hit.point;
            else
                destination = ray.GetPoint(1000);

            InstantiateProjectile(missileLaunchPoint1, missile, missileSpeed);
            InstantiateProjectile(missileLaunchPoint2, missile, missileSpeed);
            
        }


        void InstantiateProjectile(Transform firePoint, GameObject projectile, float speed)
        {
            var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
            projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * speed + spaceship.velocity;
        }


        IEnumerator LaunchCheckLaser()
        {
            isFiring = true;
            FireLaser();
            yield return new WaitForSeconds(.2f);
            isFiring = false;
            laserL.SetBool("Fire", false);
            laserR.SetBool("Fire", false);
        }
        IEnumerator LaunchCheckMissile()
        {
            isFiring = true;
            FireMissile();
            yield return new WaitForSeconds(.2f);
            isFiring = false;
            missileL.SetBool("Fire", false);
            missileR.SetBool("Fire", false);

        }
        IEnumerator LaunchCheckShockWave()
        {
            isFiring = true;
            FireShockWave();
            yield return new WaitForSeconds(.2f);
            isFiring = false;

        }



        void Update()
        {
            if (weaponHolserCD > 0.0f)
            {
                weaponHolserCD = Mathf.Clamp(weaponHolserCD - Time.deltaTime, 0.0f, weaponHolsterTimer);
            }
            else
            {
                weaponHolstered = true;
                laserL.SetBool("inCombat", false);
                laserR.SetBool("inCombat", false);
                missileL.SetBool("inCombat", false);
                missileR.SetBool("inCombat", false);
            }
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
        public void OnLaunchShockWave(InputAction.CallbackContext context)
        {
            if (isFiring == false)
            {
                StartCoroutine(LaunchCheckShockWave());
            }

        }
        #endregion

    }

}