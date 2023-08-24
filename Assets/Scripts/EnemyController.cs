using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Animator anim;
    Transform target;
    NavMeshAgent agent;

    float forwardAmount;
    float turnAmount;

    void Start()
    {
        SetupAnimator();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        forwardAmount = localVelocity.z;
        turnAmount = Mathf.Atan2(localVelocity.x, localVelocity.z);
        
        anim.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        anim.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
    }



    void SetupAnimator()
    {
        anim = GetComponent<Animator>();

        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if (childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }

    }
}
