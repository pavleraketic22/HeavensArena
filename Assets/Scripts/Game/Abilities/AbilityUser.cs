using System.Collections.Generic;
using UnityEngine;

public class AbilityUser : MonoBehaviour
{
    private Dictionary<AbilityType,IAbility> abilities = new ();
    private HashSet<AbilityType> unlockedAbilities = new();

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
        unlockedAbilities.Add(type);
    }

    /*public void ActivateAll()
    {
        foreach (var ability in abilities)
        {
            ability.Activate(gameObject);
        }
    }
    */
}