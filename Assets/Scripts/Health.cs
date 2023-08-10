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
    public string gameOver;

    
    public void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        healthBar.value = health;
    }

    public void TakeDamage(float damage)
    {
        if (player != null) {
        health -= damage;
        AudioManager.instance.Play(hurtSound, this.gameObject);


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
        AudioManager.instance.Play(gameOver, this.gameObject);
        SceneManager.LoadScene("GameOver");
    }

}
