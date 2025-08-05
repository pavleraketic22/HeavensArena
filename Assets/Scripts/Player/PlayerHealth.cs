using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    
    [SerializeField] private Image[] hearts;
    [SerializeField] private GameObject player;

    public int health;
    
    public int maxHealth = 4;
    
    
    void Start()
    {
        UpdateHealth();

    }

    public void UpdateHealth()
    {
        if (health <= 0)
        {
            Destroy(player);
        }
        
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].color = Color.red;
            }else
            {
                hearts[i].color = Color.black;
            }
        }
    }
    
    public void AddHealth(int amount)
    {
        health += amount;

        if (health > hearts.Length)
        {
            health = hearts.Length; 
        }

        UpdateHealth();
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        UpdateHealth();
    }

    public bool isFull()
    {
        if (health == maxHealth)
            return true;
        else
        {
            return false;
        }
    }
    
    
}
