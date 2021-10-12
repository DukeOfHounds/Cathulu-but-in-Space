using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public GameObject Cthulhu;
    public GameObject BossLaser;
    public GameObject dashParticle;
    public float teleportRange;
    public float sightRadius = 1000f;
    private float rotationSpeed;
    public float startRotationSpeed = .1f;
    private float bossSpeed;
    public float bossStartSpeed = 1.5f;

    private bool canRotate = true;
    private bool canMove = true;
    private bool isAttacking;
    private Vector3 destination;
    private float attackCD = 0f;
    private float attackTimer = 8f;


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
            CheckAttack();
            CurrentAttack = attackTypes.Ranged;

        }
    }

    IEnumerator ResetRangedAttack()
    {
        yield return new WaitForSeconds(2f);
        rotationSpeed = startRotationSpeed;
        BossLaser.gameObject.GetComponent<HealthAndDamage.BossLaser>().damageActive = false;
        //canMove = true;
    }
    IEnumerator ResetMovement()
    {
        yield return new WaitForSeconds(1f);
        //canRotate = true;
        //canMove = true;

    }


    private void OnTriggerEnter(Collider collision)
    {
        Vector3 spawnPoint = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f));
        if (collision.gameObject.tag == "Player")
        {
            //canMove = false;
            //canRotate = false;
            //Instantiate(dashParticle, gameObject.transform.position, gameObject.transform.rotation);
            Vector3 origin = Cthulhu.transform.position;

            if (spawnPoint.magnitude > 1)
            {
                spawnPoint.Normalize();
            }

            spawnPoint *= teleportRange;
            spawnPoint += origin;
            Instantiate(dashParticle, gameObject.transform.position, gameObject.transform.rotation);
            Cthulhu.transform.position = spawnPoint;           
        }
        
        ResetMovement();
    }
    public void Attack()
    {
        attackCD = attackTimer;
        switch (CurrentAttack)
        {
            case attackTypes.Melee:
                //perform melee attack
                break;
            case attackTypes.Tentacle:
                //perform Tentacle attack
                break;
            case attackTypes.Ranged:
                //canMove = false;
                rotationSpeed = startRotationSpeed * 4f;
                //MoveFaceTarget();
                BossLaser.gameObject.GetComponent<HealthAndDamage.BossLaser>().damageActive = true;
                StartCoroutine(ResetRangedAttack());
                break;
        }
    }

    public void CheckAttack()
    {
        if(attackCD > 0f)
        {
            attackCD = Mathf.Clamp(attackCD - Time.deltaTime, 0.0f, attackTimer);
        }
        else
        {
            Attack();
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
