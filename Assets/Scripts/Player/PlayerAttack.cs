using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private IAttackStrategy currentAttack;
    
    public Transform firePoint;
    public GameObject fireballPrefab;

    private AbilityUser abilityUser;
    
    public float fireballCooldown = 0.5f;
    private float fireballCooldownTimer = 0f;  
    private int manaCost = 1;
    
    public float lifeCooldown = 5f;
    private float lifeCooldownTimer = 0f; 
    
    public float timeCooldown = 10f;
    private float timeCooldownTimer = 0f; 
    
    public float deathCooldown = 20f;
    private float deathCooldownTimer = 0f; 

    private void Awake()
    {
        abilityUser = GetComponent<AbilityUser>();
    }

    public void SetAttackStrategy(IAttackStrategy strategy)
    {
        currentAttack = strategy;
    }

    public void PerformAttack(Vector3? position=null)
    {
        if (currentAttack != null)
            currentAttack.Attack(position);
    }

    private void Update()
    {
        // Smanjuj cooldown timer
        if (fireballCooldownTimer > 0)
            fireballCooldownTimer -= Time.deltaTime;
        
        if (lifeCooldownTimer > 0)
            lifeCooldownTimer -= Time.deltaTime;
        
        if (timeCooldownTimer > 0)
            timeCooldownTimer -= Time.deltaTime;
        
        if (deathCooldownTimer > 0)
            deathCooldownTimer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Q) && fireballCooldownTimer <= 0f) 
        {
            Stats stats = GetComponent<Stats>();
            if (stats == null)
            {
                Debug.Log("Stats not found on player.");
                return;
            }
            if (!stats.UseMana(manaCost))
            {
                Debug.Log("Not enough mana for Life Ability.");
                return;
            }
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
            float direction = transform.localScale.x > 0 ? 1 : -1; 
            SetAttackStrategy(new FireballAttack(fireball, direction));
            PerformAttack();
            
            fireballCooldownTimer = fireballCooldown; // reset cooldown
        }

        if (Input.GetKeyDown(KeyCode.W) && lifeCooldownTimer <= 0f)
        {
            abilityUser.ActivateAbility(AbilityType.Life);
            lifeCooldownTimer = lifeCooldown;
        }
        if (Input.GetKeyDown(KeyCode.E) && timeCooldownTimer <= 0f)
        {
            abilityUser.ActivateAbility(AbilityType.Time);
            timeCooldownTimer = timeCooldown;
        }
        if (Input.GetKeyDown(KeyCode.R) && deathCooldownTimer <= 0f)
        {
            abilityUser.ActivateAbility(AbilityType.Death);
            deathCooldownTimer = deathCooldown;
        }
        
    }
}

