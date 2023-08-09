// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyAttack : MonoBehaviour
// {
//     Health target;
//     [SerializeField] float damage = 10f;
//     public string attackSound;

//     void Start()
//     {
//         target = FindObjectOfType<Health>();
//     }


//     public void AttackHitEvent()
//     {
//         if (target == null) return;
//         AudioManager.instance.Play(attackSound, this.gameObject);
//         target.TakeDamage(damage);
//         Debug.Log("Hit");
//     }
// }
