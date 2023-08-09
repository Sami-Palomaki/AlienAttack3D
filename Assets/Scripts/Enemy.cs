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
    private float dist;
    private float lastAttackTime = -999f; // Alustetaan aika niin pieneksi, että vihollinen voi hyökätä heti alussa
    bool canAttack = false;
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
                anim.SetBool("isAttacking", true);
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
        agent.speed = 0f;
        anim.SetTrigger("dying");       // Vihollinen kuolee animaatio-triggeri menee päälle

    }

    public void StartAttack()
    {
        Debug.Log("Pitäisi aloittaa hyökkäys!");
        FaceTarget();       // Vihollinen katsoo sinua päin kun hyökkää
        Collider[] colliders = Physics.OverlapSphere(attackPos.position, radius);
        foreach (var col in colliders)
        {
            DoDamage();
            break;
        
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
        Health playerHealth = FindObjectOfType<Health>();
        
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage); // Käytetään "damage" -muuttujaa vihollisen aiheuttaman vahingon määrittämiseen
            Debug.Log("Tehty damagea!");
        }
        
    }

    void OnDrawGizmosSelected()                             // Vihollisen hyökkäys-gizmot
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }
}
