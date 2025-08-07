using System.Collections;
using UnityEngine;

public class DeathAbility : MonoBehaviour, IAbility
{
    public float damageRadius = 5f;
    public float damageInterval = 3f;
    public int damageAmount = 1;
    private bool isActive = false;

    public void Activate(GameObject user)
    {
        if (!isActive)
        {
            StartCoroutine(ApplyAoEDamage(user));
            isActive = true;
        }
    }

    private IEnumerator ApplyAoEDamage(GameObject user)
    {
        while (true)
        {
            // Nađi sve kolidere unutar radiusa
            Collider2D[] hits = Physics2D.OverlapCircleAll(user.transform.position, damageRadius);

            foreach (var hit in hits)
            {
                if (hit.gameObject == user)
                    continue; // Ne šteti sebi

                bool isUserPlayer = user.CompareTag("Player");
                bool isTargetPlayer = hit.CompareTag("Player");

                // Ako je user player, oduzmi zdravlje neprijateljima
                if (isUserPlayer && !isTargetPlayer)
                {
                    var enemyHealth = hit.GetComponent<EnemyBehaviour>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.health -= damageAmount;
                        Debug.Log($"Enemy {hit.name} took {damageAmount} damage from DeathAbility!");
                    }
                }
                // Ako je user enemy/boss, oduzmi zdravlje igraču
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
}