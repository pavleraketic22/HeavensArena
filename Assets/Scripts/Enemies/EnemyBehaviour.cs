using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public HealthBarBehaviour HealthBarBehaviour;
    public float health;
    public float maxHealth;

    public GameObject player;
    public float speed;
    public float customTimeScale = 1f;
    private float distance;
    public float knockbackForce = 2f;

    private void Start()
    {
        health = maxHealth;
        HealthBarBehaviour.SetHealth(health, maxHealth);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            health -= 1;
            HealthBarBehaviour.SetHealth(health, maxHealth);
            Destroy(other.gameObject);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Stats stats = FindObjectOfType<Stats>();
            stats.TakeDamage(1);
            
            Vector2 knockDir = (other.transform.position - transform.position).normalized;
            other.gameObject.GetComponent<PlayerMovement>().Knockback(knockDir, knockbackForce);
        }
        if (other.gameObject.CompareTag("Fireball"))
        {
            health -= 1;
            HealthBarBehaviour.SetHealth(health, maxHealth);
            Destroy(other.gameObject);
        }
    }

    protected virtual void Update()
    {
        if (health <= 0)
        {
            Die();
            
        }
        else
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if (distance < 4)
            {
                float delta = Time.deltaTime * customTimeScale;
                transform.position =
                    Vector2.MoveTowards(this.transform.position, player.transform.position, speed * delta);
            }
        }

        
        
        
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
