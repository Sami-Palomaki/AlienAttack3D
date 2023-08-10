using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Health : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public string hurtSound;
    public Slider healthBar;
    public GameObject player;
    Animator anim;
    bool isGameOver = false; // Lisää tämä muuttuja

    
    public void Start()
    {
        health = maxHealth;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        healthBar.value = health;
    }

    public void TakeDamage(float damage)
    {
        if (player != null && health > 0) {
        health -= damage;
        AudioManager.instance.Play(hurtSound, this.gameObject);
        }


        if (health <= 0) 
        {
            // isGameOver = true;
            anim.SetTrigger("dying");
            StartCoroutine(GameOverAfterDelay(5f));
            //GameOver();
        }
    }

    IEnumerator GameOverAfterDelay(float delay)
    {
        // Pysäytetään pelaajan liikkuminen asettamalla Rigidbody pois päältä
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
            playerRigidbody.isKinematic = true;
        }

        yield return new WaitForSeconds(delay); // Odota annettu aika
        GameOver(); // Kutsu GameOver-metodia
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
        SceneManager.LoadScene("GameOver");
    }

}
