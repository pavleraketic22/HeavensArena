using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Update = UnityEngine.PlayerLoop.Update;

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
             ability = gameObject.AddComponent<LifeAbility>();
             abilityUser.AddAbility(abilityToGive,ability);
             abilityUser.UnlockAbility(AbilityType.Life);

        }
            
        else if (abilityToGive == AbilityType.Time)
        {
             ability = gameObject.AddComponent<TimeAbility>();
             abilityUser.AddAbility(abilityToGive,ability);
             abilityUser.UnlockAbility(AbilityType.Time);

        }
           
        else if (abilityToGive == AbilityType.Death)
        {
             ability = gameObject.AddComponent<DeathAbility>();
             abilityUser.AddAbility(abilityToGive,ability);
             abilityUser.UnlockAbility(AbilityType.Death);

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

    private void Update()
    {
        if (health <= maxHealth / 2f) //moram dodati jos uslova ovo je za sve, dakle treba mi i cooldown da ne bi bilo spamovanja
        {
            abilityUser.ActivateAbility(abilityToGive);
        }
    
        
        base.Update(); 
    }
}
