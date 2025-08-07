using UnityEngine;

public class RuleManager : MonoBehaviour
{
    public static RuleManager Instance { get; private set; }

    public bool hasRuleOfTime = false;
    public bool hasRuleOfDeath = false;
    public bool hasRuleOfLife = false;
    

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void UnlockRule(AbilityType ability, AbilityUser abilityUser)
    {
        if (abilityUser == null)
        {
            Debug.LogError("AbilityUser je null! Ne moze otkljucati ability!");
            return;
        }
        switch (ability)
        {
            case AbilityType.Time:
                hasRuleOfTime = true;
                abilityUser.UnlockAbility(AbilityType.Time);
                Debug.Log("Unlocked: Rule of Time");
                break;
            case AbilityType.Death:
                hasRuleOfDeath = true;
                abilityUser.UnlockAbility(AbilityType.Death);
                Debug.Log("Unlocked: Rule of Death");
                break;
            case AbilityType.Life:
                hasRuleOfLife = true;
                abilityUser.UnlockAbility(AbilityType.Life);
                Debug.Log("Unlocked: Rule of Life");
                break;
        }

        // Možeš emitovati event ovde za UI
        // OnRuleUnlocked?.Invoke(rule);
    }

    public bool IsMasterOfRules()
    {
        return hasRuleOfTime && hasRuleOfDeath && hasRuleOfLife;
    }
}