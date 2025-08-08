using UnityEngine;

public class TimeAbility : MonoBehaviour, IAbility
{
    public float slowDuration = 5f;
    public float slowFactor = 0.5f;
    private int manaCost = 10;

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
                Debug.Log("Not enough mana for Life Ability.");
                return;
            }
            // 🎮 PLAYER koristi TimeAbility → uspori neprijatelje
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
            // 👹 ENEMY koristi TimeAbility → uspori igrača
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                    playerMovement.customTimeScale = slowFactor;

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