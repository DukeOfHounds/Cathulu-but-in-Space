using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MissleControl : MonoBehaviour
{

    public Transform LaunchPoint;

    public GameObject missile;
    public Rigidbody spaceship;
    private List<GameObject> MissilesFired = new List<GameObject>();


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
        //creates a Missle at LuanchPoint, with a rotaition facing ship forward. 
       GameObject m =  Instantiate(missile, LaunchPoint.position, missile.transform.rotation);
       MissilesFired.Add(m);
        //Vector3 V = ()
      // m.GetComponent<Rigidbody>().AddRelativeForce();
        //m.GetComponent<Rigidbody>().velocity.Set(0,0,speed);
       // m.transform.forward.Set(m.transform.forward.x, m.transform.forward.y, speed); //launches missle at set speed
       
    }
    #region Input Methods
    public void OnLaunchMissile(InputAction.CallbackContext context)
    {
        LaunchMissile();
    }
        #endregion

    }
