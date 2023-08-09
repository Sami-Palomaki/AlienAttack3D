using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 50f;
    public GameObject blood;
    Animator anim;
    UnityEngine.AI.NavMeshAgent agent;

    // Tämä metodi alustaa Animator-komponentin
    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
        Invoke("DestroyGameObject", 6f);
    }

    private void DestroyGameObject()
    {
        // Tuhotaan tämä GameObject
        Destroy(gameObject);
    }
}