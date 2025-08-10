using UnityEngine;


public class LifeAbility : MonoBehaviour, IAbility
{
    public int healAmount = 2;
    private int manaCost = 10;

    private GameObject healEffectPrefab;
    private GameObject activeEffect;

    private void Awake()
    {
        // Učitaj prefab iz Resources
        healEffectPrefab = Resources.Load<GameObject>(
            "JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Light/Heal"
        );

        if (healEffectPrefab == null)
        {
            Debug.LogError("❌ LifeAbility: Heal prefab nije pronađen u Resources!");
        }
    }

    public void Activate(GameObject user)
    {
        if (user.gameObject.CompareTag("Player"))
        {
            Stats stats = user.GetComponent<Stats>();
            if (stats == null)
            {
                Debug.Log("Stats not found on player.");
                return;
            }

            // Provera mane
            if (!stats.UseMana(manaCost))
            {
                Debug.Log("Not enough mana for Life Ability.");
                return;
            }

            Debug.Log($"{user.name} activated LIFE ability!");
            stats.Heal(healAmount);

            // Instanciraj heal efekat
            if (healEffectPrefab != null)
            {
                activeEffect = Instantiate(healEffectPrefab, user.transform.position, Quaternion.identity);
                activeEffect.transform.SetParent(user.transform);
                Destroy(activeEffect, 2f); // Efekat traje 2 sekunde (možeš promeniti)
            }
        }
        else
        {
            HealthBarBehaviour health = user.GetComponent<HealthBarBehaviour>();
            EnemyBehaviour enemy = user.GetComponent<EnemyBehaviour>();

            enemy.health += 1;
            Debug.Log($"Posle heala: {enemy.health}");

            if (enemy.health > enemy.maxHealth)
                enemy.health = enemy.maxHealth;

            health.SetHealth(enemy.health, enemy.maxHealth);

            // Efekat i na neprijatelja
            if (healEffectPrefab != null)
            {
                activeEffect = Instantiate(healEffectPrefab, user.transform.position, Quaternion.identity);
                activeEffect.transform.SetParent(user.transform);
                Destroy(activeEffect, 2f);
            }
        }
    }
}
