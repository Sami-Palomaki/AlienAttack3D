using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{

    public float health = 50f;
    public GameObject blood;
    public Slider healthBar;

    Animator anim;
    UnityEngine.AI.NavMeshAgent agent;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        healthBar.value = health;
    }

    public void TakeDamage(float amount)
    {
        Instantiate(blood, transform.position, Quaternion.identity);
        health -= amount;
        if (health <= 0)
        {
            anim.SetBool("isDying", true);
            Die();
        }
    }

    void Die ()
    {
        agent.isStopped = true; // Pysäyttää NavMeshAgentin liikkumisen
        Invoke("DestroyGameObject", 35f);
    }

    private void DestroyGameObject()
    {
        // Tuhotaan tämä GameObject
        Destroy(gameObject);
    }
}