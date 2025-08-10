using System.Collections;
using UnityEngine;


public class DeathAbility : MonoBehaviour, IAbility
{
    private GameObject deathEffectPrefab;
    private GameObject activeEffect;

    public float damageRadius = 5f;
    public float damageInterval = 1f;
    public int damageAmount = 1;
    public int manaCost = 2;

    private bool isActive = false;
    private Coroutine damageCoroutine;
    private Coroutine manaCoroutine;
    private GameObject currentUser;

    private void Awake()
    {
        // Učitavamo prefab iz Resources foldera
        deathEffectPrefab = Resources.Load<GameObject>(
            "JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Eerie/Souls"
        );

        if (deathEffectPrefab == null)
        {
            Debug.LogError("❌ DeathAbility: Prefab nije pronađen u Resources!");
        }
    }

    public void Activate(GameObject user)
    {
        bool isUserPlayer = user.CompareTag("Player");

        if (isUserPlayer)
        {
            if (!isActive)
            {
                Stats stats = user.GetComponent<Stats>();
                if (stats == null || stats.CurrentMana < manaCost)
                {
                    Debug.Log("Not enough mana to activate Death Ability!");
                    return;
                }

                // Instanciraj efekat ako prefab postoji
                if (deathEffectPrefab != null)
                {
                    activeEffect = Instantiate(deathEffectPrefab, user.transform.position, Quaternion.identity);
                    activeEffect.transform.SetParent(user.transform);
                    Debug.Log("✅ Death effect instantiated and parented to user.");
                }

                currentUser = user;
                damageCoroutine = StartCoroutine(ApplyAoEDamage(user));
                manaCoroutine = StartCoroutine(DrainManaPeriodically(stats));
                isActive = true;
            }
            else
            {
                Stop();
            }
        }
        else
        {
            if (!isActive)
            {
                if (deathEffectPrefab != null)
                {
                    activeEffect = Instantiate(deathEffectPrefab, user.transform.position, Quaternion.identity);
                    activeEffect.transform.SetParent(user.transform);
                    Debug.Log("✅ Death effect instantiated and parented to user.");
                }
                currentUser = user;
                damageCoroutine = StartCoroutine(ApplyAoEDamage(user));
                isActive = true;
            }
        }
    }

    public void Stop()
    {
        if (isActive)
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
            if (manaCoroutine != null)
            {
                StopCoroutine(manaCoroutine);
                manaCoroutine = null;
            }

            if (activeEffect != null)
            {
                Destroy(activeEffect);
                activeEffect = null;
            }

            isActive = false;
            Debug.Log("Death ability stopped!");
        }
    }

    private IEnumerator ApplyAoEDamage(GameObject user)
    {
        while (true)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(user.transform.position, damageRadius);

            foreach (var hit in hits)
            {
                if (hit.gameObject == user) continue;

                bool isUserPlayer = user.CompareTag("Player");
                bool isTargetPlayer = hit.CompareTag("Player");

                if (isUserPlayer && !isTargetPlayer)
                {
                    var enemyHealth = hit.GetComponent<EnemyBehaviour>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.health -= damageAmount;
                        enemyHealth.HealthBarBehaviour.SetHealth(enemyHealth.health, enemyHealth.maxHealth);
                        Debug.Log($"Enemy {hit.name} took {damageAmount} damage from DeathAbility!");
                    }
                }
                else if (!isUserPlayer && isTargetPlayer)
                {
                    var playerStats = hit.GetComponent<Stats>();
                    if (playerStats != null)
                    {
                        playerStats.TakeDamage(damageAmount);
                        Debug.Log($"Player took {damageAmount} damage from DeathAbility!");
                    }
                }
            }

            yield return new WaitForSeconds(damageInterval);
        }
    }

    private IEnumerator DrainManaPeriodically(Stats stats)
    {
        while (true)
        {
            if (stats.CurrentMana < manaCost)
            {
                Debug.Log("Mana ran out, stopping Death Ability.");
                Stop();
                yield break;
            }
            else
            {
                stats.UseMana(manaCost);
            }

            yield return new WaitForSeconds(damageInterval);
        }
    }
}

