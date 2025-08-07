using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RuleGem : MonoBehaviour
{
    [FormerlySerializedAs("ruleType")] public AbilityType abilityType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        if (other.CompareTag("Player"))
        {
            AbilityUser abilityUser = other.GetComponent<AbilityUser>();
            RuleManager.Instance.UnlockRule(abilityType, abilityUser);
            Health health = FindObjectOfType<Health>();
            health.AddGem();
            Destroy(gameObject);
        }
    }
}
