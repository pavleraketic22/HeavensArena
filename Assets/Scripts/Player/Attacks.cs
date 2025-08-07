using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class FireballAttack :MonoBehaviour, IAttackStrategy
{
    private GameObject fireball;
    private float direction;
    public float speed = 5f;

    public FireballAttack(GameObject fireball, float direction)
    {
        this.fireball = fireball;
        this.direction = direction;
    }

    public void Attack(Vector3? targetPosition = null)
    {
        
        // pomeranje metka desno ili levo, zavisno gde igrač gleda
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction * speed, 0);
        fireball.GetComponent<SpriteRenderer>().flipX = direction < 0;
        

    }
    
}


