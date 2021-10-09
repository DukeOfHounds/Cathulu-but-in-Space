using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public GameObject Cthulhu;
    public float teleportRange;
    public float sightRadius = 1000f;
    public float rotationSpeed;
    private float startRotationSpeed = .1f;
    public float bossSpeed;
    private float bossStartSpeed = 1.5f;

    private bool canRotate = true;
    private bool canMove = true;
    private bool isAttacking;
    private Vector3 destination;

    private enum attackTypes {Melee, Ranged, Tentacle };
    attackTypes CurrentAttack;
    
    Transform target;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        cam = PlayerManager.instance.cam;
        rotationSpeed = startRotationSpeed;
        bossSpeed = bossStartSpeed;
    }

   

    // Update is called once per frame
    void Update()
    {
        float targetDistance = Vector3.Distance(target.position, transform.position);

        if (targetDistance <= sightRadius)
        {
            MoveFaceTarget();

            if (targetDistance <= sightRadius / 1.1)
            {
                CurrentAttack = attackTypes.Tentacle;

                if (targetDistance <= sightRadius / 2)
                {
                    CurrentAttack = attackTypes.Ranged;


                    if (targetDistance <= sightRadius / 4)
                    {
                        CurrentAttack = attackTypes.Melee;
                        DashAway();
                    }
                }
            }
        }
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(2f);
        bossSpeed = bossStartSpeed;
    }

    public void DashAway()
    {
        //bossSpeed = bossSpeed * 2f;
        //Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //destination = ray.GetPoint(4000);
        //transform.position = Vector3.MoveTowards(transform.position, destination, bossSpeed);
        //StartCoroutine(ResetSpeed()); //Move Away From Player, Not Workiing
    }
    private void OnTriggerEnter(Collider collision)
    {
        Vector3 spawnPoint = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f));
        if (collision.gameObject.tag == "Player")
        {
            canMove = false;

            Vector3 origin = Cthulhu.transform.position;

            if (spawnPoint.magnitude > 1)
            {
                spawnPoint.Normalize();
            }

            spawnPoint *= teleportRange;
            spawnPoint += origin;
        }
        Cthulhu.transform.position = spawnPoint;
        canMove = true;
    }
    public void Attack()
    {
        switch (CurrentAttack)
        {
            case attackTypes.Melee:
                //perform melee attack
                break;
            case attackTypes.Tentacle:
                //perform Tentacle attack
                break;
            case attackTypes.Ranged:
                //perform Ranged attack
                break;
        }
    }
    public void MoveFaceTarget()
    {
        if (canRotate)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, bossSpeed);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}
