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
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
            float direction = transform.localScale.x > 0 ? 1 : -1; 
            SetAttackStrategy(new FireballAttack(fireball, direction));
            PerformAttack();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            abilityUser.ActivateAbility(AbilityType.Life);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            abilityUser.ActivateAbility(AbilityType.Life);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            abilityUser.ActivateAbility(AbilityType.Life);
        }
        
    }
}

