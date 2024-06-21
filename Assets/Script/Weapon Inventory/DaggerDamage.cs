using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DaggerDamage : MonoBehaviour
{
    public int damage = 5; // Amount of damage the dagger deals
    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is an enemy
        if (other.CompareTag("Enemy"))
        {
            // Get the EnemyBehavior component from the enemy GameObject
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();

            // If the enemy component is found, apply damage
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy hit! Damage applied: " + damage);
            }
        }
    }
}
