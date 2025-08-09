using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    private int damage = 5;
    public HealthBarBehaviour healthBarBehaviour;
    private EnemyBehaviour enemy;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();
        healthBarBehaviour = GetComponentInParent<HealthBarBehaviour>();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.health -= 5;
            healthBarBehaviour.SetHealth(enemy.health,enemy.maxHealth);
        }
    }
}
