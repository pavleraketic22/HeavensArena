using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enviroment
{
    public class HeartPickup : MonoBehaviour
    {
        private int healAmount = 1;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            Health playerHealth = other.GetComponent<Health>();
            if (other.CompareTag("Player") && playerHealth.CurrentHealth != playerHealth.MaxHealth)
            {
                playerHealth.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}

