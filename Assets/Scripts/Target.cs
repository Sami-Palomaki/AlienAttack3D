using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 50f;
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
            health -= amount;
            if (health <= 0)
            {
                anim.SetBool("isDying", true);
                Die();
            }
    }

    void Die ()
    {
        agent.speed = 0f;
        Invoke("DestroyGameObject", 6f);
    }

    private void DestroyGameObject()
    {
        // Tuhotaan tämä GameObject
        Destroy(gameObject);
    }
}