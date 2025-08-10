using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private IAttackStrategy currentAttack;
    
    private SpriteRenderer spriteRenderer;
    
    public Transform firePoint;
    public GameObject fireballPrefab;

    private AbilityUser abilityUser;
    
    public float fireballCooldown = 1f;
    public float fireballCooldownTimer = 0f;  
    private int manaCost = 1;
    
    public float lifeCooldown = 5f;
    public float lifeCooldownTimer = 0f; 
    
    public float timeCooldown = 10f;
    public float timeCooldownTimer = 0f; 
    
    public float deathCooldown = 20f;
    public float deathCooldownTimer = 0f;


    public bool fireballActivated = false;
    public bool lifeActivated = false;
    public bool timeActivated = false;
    public bool deathActivated = false;
    

    private void Awake()
    {
        abilityUser = GetComponent<AbilityUser>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
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
            float direction = spriteRenderer.flipX ? -1 : 1; 
            SetAttackStrategy(new FireballAttack(fireball, direction));
            PerformAttack();
            Music.Instance.PlaySFX("Fire",0.7f);
            fireballActivated = true;

            fireballCooldownTimer = fireballCooldown; 
        }

        if (Input.GetKeyDown(KeyCode.W) && lifeCooldownTimer <= 0f && RuleManager.Instance.hasRuleOfLife)
        {
            abilityUser.ActivateAbility(AbilityType.Life);
            Music.Instance.PlaySFX("Heal",0.7f);
            lifeActivated = true;
            lifeCooldownTimer = lifeCooldown;
        }
        if (Input.GetKeyDown(KeyCode.E) && timeCooldownTimer <= 0f && RuleManager.Instance.hasRuleOfTime)
        {
            abilityUser.ActivateAbility(AbilityType.Time);
            Music.Instance.PlaySFX("Time",0.7f);
            timeActivated = true;
            timeCooldownTimer = timeCooldown;
        }
        if (Input.GetKeyDown(KeyCode.R) && deathCooldownTimer <= 0f && RuleManager.Instance.hasRuleOfDeath)
        {
            abilityUser.ActivateAbility(AbilityType.Death);
            Music.Instance.PlaySFX("Poison",0.7f);
            deathActivated = true;
            deathCooldownTimer = deathCooldown;
        }
        
    }
    
    
}

