using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace HealthAndDamage
{
    [RequireComponent(typeof(Rigidbody))]
    public class Spaceship : MonoBehaviour
    {
        public float maxSpeed = 1;
        public Canvas pauseScreen;
        public GameManager gM;


        [Header("=== Ship Movement Settings ===")]
        [SerializeField]
        private float yawTorque = 500f;// right left turning force
        [SerializeField]
        private float pitchTorque = 1000f;// up down turning force
        [SerializeField]
        private float rollTorque = 1000f; // rolling force
        [SerializeField]
        private float thrust = 100f; // forward backward force
        [SerializeField]
        private float upThrust = 50f; // up down force
        [SerializeField]
        private float strafeThrust = 50f; // right left force

        [Header("=== Boost Settings ===")]
        [SerializeField]
        public float maxBoostAmount = 2f;//tank of gas
        [SerializeField]
        private float boostDeprecationRate = 0.25f;//fuel used
        [SerializeField]
        private float boostRechargRate = 0.5f;//gas refill
        [SerializeField]
        private float boostMultiplier = 5f;//speed increase
        [SerializeField]
        public bool boosting = false;// bool indicator of boost
        [SerializeField]
        public float currentBoostAmount;// amount of current fuel

        [Header("=== Drag/Speed Reduction Settings ===")]
        [SerializeField, Range(0.001f, 0.999f)]
        private float upDownGlideReduction = 0.111f; // up down percentile decrease 
        [SerializeField, Range(0.001f, 0.999f)]
        private float leftRightGlideReduction = 0.111f;// left right percentile decrease 
        [SerializeField, Range(0.001f, 0.999f)]
        private float thrustGlideReduction = 0.111f;// Forward backwards percentile decrease 
        [SerializeField, Range(0.001f, 0.999f)]
        private float rollGlideReduction = 0.111f;// roll percentile decrease 
        private float forwardGlide, verticalGlide, horizontalGlide, rollGlide = 0f;

        [Header("=== Drag/Speed Reduction Settings ===")]
        [SerializeField]
        private float health = 10; // Total Player Health

        Rigidbody rb;
        //   GameObject ship;

        private float thrust1D, upDown1D, strafe1D, roll1D;
        private Vector2 pitchYaw;
        public bool alive = true;

        // Start is called before the first frame update
        void Start()
        {

            rb = GetComponent<Rigidbody>();
            //currentBoostAmount = maxBoostAmount;
            Cursor.visible = false; // hides cursor 
            Cursor.lockState = CursorLockMode.Confined;// locks cursor to game window
            if(gM.tutorial == true)
            {
                StartTutorial();
                StartCoroutine(EndTutorial());
            }

        }


        void FixedUpdate()
        {

            if (alive)
            {
                HandleBoosting();
                HandleMovement();
                HandleHealth();
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
        }
        public void TakeDamage(float damage)
        {
            health -= damage;
        }
        void HandleHealth() // regularly checks for player death
        {
            if (health <= 0)
            {
                FindObjectOfType<GameManager>().GameOver();
            }
        }
        void HandleBoosting()// controls the fuel tank depreciation and  recharge, aswell as the Boosting bool
        {
            if (boosting && currentBoostAmount > 0f)// if fuel tank is not empty & you want to boost
            {
                currentBoostAmount -= boostDeprecationRate;// deprciate fuel 
                if (currentBoostAmount <= 0f)// if empty fuel tank
                {
                    boosting = false;// stop boosing
                }
            }
            else //when not boosting or empty tank
            {
                if (currentBoostAmount < maxBoostAmount)// if fuel tank is not full
                {
                    currentBoostAmount += boostRechargRate;// refill fuel 
                }
            }
        }
        void HandleMovement() // controls 3d forces on ship, and controls speed reduction
        {
            //Roll
            if (roll1D > 0.1f || roll1D < -0.1f)
            {

                // current direction and speed, vector of change, force applied, per tic
                rb.AddRelativeTorque(Vector3.back * roll1D * rollTorque * Time.deltaTime);
                rollGlide = upDown1D * upThrust;
            }
            else
            {
                // current direction and Speed, speedReduction, per tic
                rb.AddRelativeForce(Vector3.back * rollGlideReduction * Time.deltaTime);


            }
            //Pitch aka nose up + / nose down -
            rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(-pitchYaw.y, -1f, 1f) * pitchTorque * Time.deltaTime);// current direction and Speed, vector of change, force applied, per tic
                                                                                                                   //Yaw aka nose right + /nose left -
            rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(pitchYaw.x, -1f, 1f) * yawTorque * Time.deltaTime);// current direction and Speed, vector of change, force applied, per tic

            //Thrust
            if (thrust1D > 0.1f || thrust1D < -0.1f) // if there is a force forward, or backwards
            {
                float currentThrust; //sum of forces forward/backwards

                if (boosting) // if ship is boosting 
                {
                    currentThrust = thrust * boostMultiplier;// thrust force times boost amount
                }
                else// if ship is not boosting
                {
                    currentThrust = thrust;// thrust force
                }
                rb.AddRelativeForce(Vector3.forward * thrust1D * currentThrust * Time.deltaTime);// current direction and speed, vector of change, force applied, per tic

                forwardGlide = thrust; // last force 
            }
            else// if no forward/backward force, glide ship to a stop.
            {
                // current direction and speed, force applied, per tic
                rb.AddRelativeForce(Vector3.forward * forwardGlide * Time.deltaTime);

                forwardGlide *= thrustGlideReduction;// reduces force applied over time by set reduction
            }
            // Up/Down
            if (upDown1D > 0.1f || upDown1D < -0.1f)  // if there is a force up, or down
            {

                // current direction and speed, vector of change, force applied, per tic
                rb.AddRelativeForce(Vector3.up * upDown1D * upThrust * Time.deltaTime);
                verticalGlide = upDown1D * upThrust;// last force 
            }
            else // if no up/down force, glide ship to a stop.
            {
                // current direction and speed, force applied, per tic
                rb.AddRelativeForce(Vector3.up * verticalGlide * Time.deltaTime);

                verticalGlide *= upDownGlideReduction;// reduces force applied over time by set reduction
            }
            // Strafing
            if (strafe1D > 0.1f || strafe1D < -0.1f)
            {
                // current direction and speed, vector of change, force applied, per tic
                rb.AddRelativeForce(Vector3.right * strafe1D * upThrust * Time.deltaTime);
                horizontalGlide = strafe1D * strafeThrust;// last force 
            }
            else// if no right/left force, glide ship to a stop.
            {
                // current direction and speed, force applied, per tic
                rb.AddRelativeForce(Vector3.right * horizontalGlide * Time.deltaTime);

                horizontalGlide *= leftRightGlideReduction; // reduces force applied over time by set reduction
            }
        }

        void Stabalize()// Eliminates all angular momentum
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }

        public void PowerSwitch()
        {

        }

        public void StartTutorial()
        {
            thrust1D = Mathf.Lerp(0f, 2.2f, 2f);
        }

        IEnumerator EndTutorial()
        {
            yield return new WaitForSeconds(6f);
            gM.tutorial = false;
            thrust1D = 0;
            this.gameObject.GetComponent<ProjectileControl>().Unholster();
            
        }
        //interacts with the player controler for key bindings and input values
        #region Input Methods
        public void OnThrust(InputAction.CallbackContext context)
        {
            if(gM.tutorial == false)
            {
                thrust1D = context.ReadValue<float>();
            }
        }
        public void OnStrafe(InputAction.CallbackContext context)
        {
           if(gM.tutorial == false)
           {
                strafe1D = context.ReadValue<float>();
           }
        }
        public void OnUpDown(InputAction.CallbackContext context)
        {
           if(gM.tutorial == false)
           {
                upDown1D = context.ReadValue<float>();
           }
        }
        public void OnRoll(InputAction.CallbackContext context)
        {
            if(gM.tutorial == false)
            {
                roll1D = context.ReadValue<float>();
            }
        }
        public void OnPitchYaw(InputAction.CallbackContext context)
        {
            if(gM.tutorial == false)
            {
                pitchYaw = context.ReadValue<Vector2>();
            }
        }
        public void OnStabalize(InputAction.CallbackContext context)
        {
            if(gM.tutorial == false)
            {
                Stabalize();
            }
        }

        public void OnBoost(InputAction.CallbackContext context)
        {
            if(gM.tutorial == false)
            {
                if (currentBoostAmount > 0)
                {
                    boosting = context.performed;
                }         
            }
        }
        public void OnPause(InputAction.CallbackContext context)
        {
            //if(gM.tutorial == false)
            //{
            //    sP = true;
            //    pauseScreen.gameObject.SetActive(true);
            //    Time.timeScale = 0;
            //}


        }
        #endregion
    }
}