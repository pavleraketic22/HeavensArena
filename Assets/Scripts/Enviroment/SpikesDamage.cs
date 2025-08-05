using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesDamage : MonoBehaviour
{
    public float knockbackForce = 6f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health health = FindObjectOfType<Health>();
            health.TakeDamage(1);

            Vector2 knockDir = (other.transform.position - transform.position).normalized;
            other.GetComponent<PlayerMovement>().Knockback(knockDir, knockbackForce);
        }
    }
}
