using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float maxSpeed = 5;
    public float radius = 1;
    public bool isWalking = false;
    public int damage = 15;
    public float attackCD;
    public Transform attackPos;
    public bool isAttacking;
    public string zombieSound;
    private float dist;
    private float lastAttackTime = 2f; // Alustetaan aika niin pieneksi, että vihollinen voi hyökätä heti alussa
    bool canAttack = false;
    bool isDead = false;
    NavMeshAgent agent;
    Animator anim;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        
        if (isWalking)
        {
            agent.speed = maxSpeed / 2;
        }
        else
        {
            agent.speed = maxSpeed;
        }
    }

    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);


        if (dist <= agent.stoppingDistance)
        {
            if (Time.time - lastAttackTime > attackCD) // Tarkistetaan, onko kulunut tarpeeksi aikaa viime hyökkäyksestä
            {
                lastAttackTime = Time.time; // Päivitetään viime hyökkäyksen aika
                //anim.SetBool("isAttacking", true);
                //anim.SetTrigger("attack");
                
                StartAttack();
            }
            else
            {
                anim.SetBool("isAttacking", false);
            }
        }
        agent.SetDestination(target.position);
    }

    public void Death()             
    {
        isDead = true;
        agent.speed = 0f;
        anim.SetTrigger("isDying");       // Vihollinen kuolee animaatio-triggeri menee päälle

    }

    public void StartAttack()
    {
        if (isDead)
        {
            return;
        }
        else
        {

            AudioManager.instance.Play(zombieSound, this.gameObject);
            FaceTarget();       // Vihollinen katsoo sinua päin kun hyökkää
        
            anim.SetTrigger("attack");
            DoDamage();
            
        }
    }

    public void AttackAnimationEvent()
    {
        Health playerHealth = FindObjectOfType<Health>();
        
        if (playerHealth != null)
        {    
            playerHealth.TakeDamage(damage);
        }  
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    public void DoDamage()
    {
        AttackAnimationEvent();
    }

    void OnDrawGizmosSelected()                             // Vihollisen hyökkäys-gizmot
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }
}
