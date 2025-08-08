using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour
{
    private int manaAmount = 5;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Stats playerStats = other.GetComponent<Stats>();
        if (other.CompareTag("Player") && playerStats.CurrentMana != playerStats.MaxMana)
        {
            playerStats.AddMana(manaAmount);
            Destroy(gameObject);
        }
    }
}
