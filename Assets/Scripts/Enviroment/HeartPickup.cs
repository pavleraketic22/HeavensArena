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
            Stats playerStats = other.GetComponent<Stats>();
            if (other.CompareTag("Player") && playerStats.CurrentHealth != playerStats.MaxHealth)
            {
                playerStats.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}

