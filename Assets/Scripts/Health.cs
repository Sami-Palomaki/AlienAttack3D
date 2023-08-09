using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public string hurtSound;
    public HealthBar healthBar;
    public GameObject player;
    
    public void Start()
    {
        health = maxHealth;
        // healthBar.SetMaxHealth(maxHealth);
    }

    public void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if (player != null) {
        health -= damage;
        AudioManager.instance.Play(hurtSound, this.gameObject);

        // healthBar.SetHealth(health);

        if (health <= 0) 
        {
            GameOver();         
        }
    }
    }
    public void Heal(int amount)
    {
        if(player !=null && health < maxHealth){
        health += amount;


        if (health > maxHealth)
        {
            health = maxHealth;
        }
        }
    }

    void GameOver()
    {
        
        SceneManager.LoadScene(2);
        Time.timeScale = 0;
        AudioListener.pause = true;
        // GetComponent<PlayerMovement>().enabled = false;
        // FindObjectOfType<GameManager>()?.EndGame();
    }

}
