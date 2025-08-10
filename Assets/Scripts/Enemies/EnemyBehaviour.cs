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
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    public float detectionRange = 4f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    
    public float customTimeScale = 1f;
    
    private float distance;
    public float knockbackForce = 2f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        HealthBarBehaviour.SetHealth(health, maxHealth);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            health -= 1;
            Music.Instance.PlaySFX("EnemyDamage",0.7f);
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
            Music.Instance.PlaySFX("EnemyDamage",0.7f);
            HealthBarBehaviour.SetHealth(health, maxHealth);
            Destroy(other.gameObject);
        }
    }

    protected virtual void Update()
    {
        if (health <= 0)
        {
            Music.Instance.PlaySFX("EnemyDeath",0.5f);
            Die();
            return;
            
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log("Distance to player: " + distance);
        if (distance < detectionRange)
        {
            Debug.Log("Chasing player...");
            ChasePlayer();
        }
        
    }
    
    private void ChasePlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Trčanje
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Ako igrač nije na istoj visini, pokušaj skok
        if (isGrounded && Mathf.Abs(player.transform.position.y - transform.position.y) > 0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
