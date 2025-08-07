using System.Collections.Generic;
using UnityEngine;

public class AbilityUser : MonoBehaviour
{
    private Dictionary<AbilityType,IAbility> abilities = new ();
    private HashSet<AbilityType> unlockedAbilities = new ();

    public void AddAbility(AbilityType type,IAbility ability)
    {
        abilities[type]= ability;
       
    }

    public void ActivateAbility(AbilityType type)
    {
        if (unlockedAbilities.Contains(type) && abilities.ContainsKey(type))
        {
            abilities[type].Activate(gameObject);
        }
    }


    public void UnlockAbility(AbilityType type)
    {
        if (!unlockedAbilities.Contains(type))
        {
            unlockedAbilities.Add(type);

            // Kreiranje i dodavanje konkretne instance ability-ja
            if (!abilities.ContainsKey(type))
            {
                IAbility abilityInstance = null;

                switch (type)
                {
                    case AbilityType.Life:
                        abilityInstance = gameObject.AddComponent<LifeAbility>();
                        break;
                    case AbilityType.Time:
                        abilityInstance = gameObject.AddComponent<TimeAbility>();
                        break;
                    case AbilityType.Death:
                        abilityInstance = gameObject.AddComponent<DeathAbility>();
                        break;
                }

                if (abilityInstance != null)
                {
                    abilities[type] = abilityInstance;
                    Debug.Log($"Ability {type} instance created and added.");
                }
            }

            Debug.Log("Unlocked: Rule of " + type);
        }
    }

    /*
    public void UnlockAbility(AbilityType type)
    {
        unlockedAbilities.Add(type);
        foreach (var abilityType in unlockedAbilities)
        {
            Debug.Log(abilityType);
        }
        Debug.Log("Unlocked: Rule of" + type.ToString());
    }

   */
}