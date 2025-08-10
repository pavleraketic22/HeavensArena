using UnityEngine;

public class TimeAbility : MonoBehaviour, IAbility
{
    public float slowDuration = 5f;
    public float slowFactor = 0.5f;
    private int manaCost = 10;

    private GameObject timeEffectPrefab;
    private GameObject activeEffect;

    private void Awake()
    {
        // Učitaj prefab iz Resources foldera
        timeEffectPrefab = Resources.Load<GameObject>(
            "JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Magic Misc/Time"
        );

        if (timeEffectPrefab == null)
        {
            Debug.LogError("❌ TimeAbility: Time prefab nije pronađen u Resources!");
        }
    }

    public void Activate(GameObject user)
    {
        Debug.Log($"{user.name} activated TIME ability!");

        if (user.CompareTag("Player"))
        {
            Stats stats = user.GetComponent<Stats>();
            if (stats == null)
            {
                Debug.Log("Stats not found on player.");
                return;
            }
            if (!stats.UseMana(manaCost))
            {
                Debug.Log("Not enough mana for Time Ability.");
                return;
            }

            // Instanciraj vizuelni efekat na playeru (možeš i poziciju menjati)
            if (timeEffectPrefab != null)
            {
                if (activeEffect != null)
                {
                    Destroy(activeEffect);
                }
                activeEffect = Instantiate(timeEffectPrefab, user.transform.position, Quaternion.identity);
                activeEffect.transform.SetParent(user.transform);
                Destroy(activeEffect, slowDuration + 0.5f); // uništi efekat nakon trajanja usporenja
            }

            // Usponji neprijatelje
            EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();

            foreach (EnemyBehaviour enemy in enemies)
            {
                if (enemy != null && enemy.gameObject != user)
                    enemy.customTimeScale = slowFactor;
            }

            user.GetComponent<MonoBehaviour>().StartCoroutine(RestoreTimeAfterDelay(enemies));
        }
        else
        {
            // Enemy koristi time ability → uspori igrača
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                    playerMovement.customTimeScale = slowFactor;

                // Efekat na neprijatelju (koji koristi ability)
                if (timeEffectPrefab != null)
                {
                    if (activeEffect != null)
                    {
                        Destroy(activeEffect);
                    }
                    activeEffect = Instantiate(timeEffectPrefab, user.transform.position, Quaternion.identity);
                    activeEffect.transform.SetParent(user.transform);
                    Destroy(activeEffect, slowDuration + 0.5f);
                }

                user.GetComponent<MonoBehaviour>().StartCoroutine(RestorePlayerAfterDelay(playerMovement));
            }
        }
    }

    private System.Collections.IEnumerator RestoreTimeAfterDelay(EnemyBehaviour[] enemies)
    {
        yield return new WaitForSeconds(slowDuration);

        foreach (EnemyBehaviour enemy in enemies)
        {
            if (enemy != null)
                enemy.customTimeScale = 1f;
        }
    }

    private System.Collections.IEnumerator RestorePlayerAfterDelay(PlayerMovement player)
    {
        yield return new WaitForSeconds(slowDuration);

        if (player != null)
            player.customTimeScale = 1f;
    }
}
