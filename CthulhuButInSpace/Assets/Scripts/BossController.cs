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
    public Animator animator;

    private bool canRotate = true;
    private bool canMove = true;
    private bool isAttacking;
    private Vector3 destination;
    private float attackCD = 0f;
    public float attackTimer = 12f;
    private bool dead = false;
    private GameObject gameManager;

    private enum attackTypes { Melee, Ranged, Tentacle, Wait };
    attackTypes CurrentAttack;

    Transform target;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        target = PlayerManager.instance.player.transform;
        cam = PlayerManager.instance.cam;
        rotationSpeed = startRotationSpeed;
        bossSpeed = bossStartSpeed;
    }



    // Update is called once per frame
    void Update()
    {
        float targetDistance = Vector3.Distance(target.position, transform.position);

        if (dead == false)
        {
            if (targetDistance <= sightRadius)
            {
                
                //if(gameManager.GetComponent<AudioSource>().clip == gameManager.GetComponent<>
                //{

                //}
                MoveFaceTarget();
                CheckAttack();

            }
        }
    }

    IEnumerator Wait(float waitTime)
    {
        attackCD = attackTimer;
        rotationSpeed = 0;
        bossSpeed = 0;
        yield return new WaitForSeconds(waitTime);
        bossSpeed = bossStartSpeed;
        rotationSpeed = startRotationSpeed;
        attackCD = attackTimer;
    }

    IEnumerator ResetRangedAttack()
    {
        yield return new WaitForSeconds(2f);
        rotationSpeed = startRotationSpeed;
        BossLaser.gameObject.GetComponent<HealthAndDamage.BossLaser>().damageActive = false;
        //canMove = true;
        attackCD = attackTimer;
    }
    IEnumerator LaserAttack()
    {
        animator.SetTrigger("Attack");
        rotationSpeed = startRotationSpeed * 4f;
        yield return new WaitForSeconds(2f);
        
        //canMove = false;
        //MoveFaceTarget();
        BossLaser.gameObject.GetComponent<HealthAndDamage.BossLaser>().damageActive = true;
        StartCoroutine(ResetRangedAttack());

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
            attackCD = attackCD - 2; 
            }


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
                StartCoroutine(LaserAttack());            
                break;
            case attackTypes.Wait:
                StartCoroutine(Wait(6f));
                break;
        }
    }

    public void CheckAttack()
    {
        bool random = (Random.value > 0.5f);
        if (attackCD > 0f)
        {
            attackCD = Mathf.Clamp(attackCD - Time.deltaTime, 0.0f, attackTimer);
        }
        else
        {
            //if (random)
               CurrentAttack = attackTypes.Ranged;
            //else
                //CurrentAttack = attackTypes.Wait;
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


    public void Death()
    {
        dead = true;
        animator.SetBool("Death", true);
        rotationSpeed = 0;
        bossSpeed = 0;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}
