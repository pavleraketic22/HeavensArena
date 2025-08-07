using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BossBehaviour : EnemyBehaviour
{
    public GameObject gemPrefab;
    [FormerlySerializedAs("ruleToGive")] public AbilityType abilityToGive;
    private AbilityUser abilityUser;

    void Start()
    {
        health = maxHealth;
        HealthBarBehaviour.SetHealth(health, maxHealth);
        
        abilityUser = GetComponent<AbilityUser>();
        IAbility ability = null;
        
        if (abilityToGive == AbilityType.Life)
        {
             ability = new LifeAbility();
        }
            
        else if (abilityToGive == AbilityType.Time)
        {
             ability = new TimeAbility();
        }
           
        else if (abilityToGive == AbilityType.Death)
        {
             ability = new DeathAbility();
        }

        if (ability != null)
        {
            abilityUser.AddAbility(abilityToGive,ability);
        }
            
    }
    protected override void Die()
    {
        DropGem();
        base.Die();
    }

    private void DropGem()
    {
        Instantiate(gemPrefab, transform.position, Quaternion.identity);
    }
}
