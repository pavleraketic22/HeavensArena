using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public HealthBarBehaviour HealthBarBehaviour;
    public float health;
    public float maxHealth = 5;

    private void Start()
    {
        health = maxHealth;
        HealthBarBehaviour.SetHealth(health, maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            health -= 1;
            HealthBarBehaviour.SetHealth(health, maxHealth);
            Destroy(other.gameObject);
        }
    }
    
}
