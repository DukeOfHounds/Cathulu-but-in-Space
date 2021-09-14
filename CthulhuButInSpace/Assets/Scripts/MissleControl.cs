using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MissleControl : MonoBehaviour
{

    public Transform LaunchPoint;

    public GameObject missile;


    [Header("=== Missle Settings ===")]
    [SerializeField]
    private float speed;


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
        
        Instantiate(missile, LaunchPoint.position, missile.transform.rotation);
    }
    #region Input Methods
    public void OnLaunchMissile(InputAction.CallbackContext context)
    {
        LaunchMissile();
    }
        #endregion

    }
