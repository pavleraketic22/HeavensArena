using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRuleCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (RuleManager.Instance.hasRuleOfLife)
            {
                Destroy(gameObject);
            }
        }
    }
}
