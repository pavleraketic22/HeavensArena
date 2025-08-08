using System.Collections;
using UnityEngine;
/*
public class DeathAbility : MonoBehaviour, IAbility
{
    public float damageRadius = 5f;
    public float damageInterval = 1f;
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
                        enemyHealth.HealthBarBehaviour.SetHealth(enemyHealth.health,enemyHealth.maxHealth); 
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
*/

using System.Collections;
using UnityEngine;

public class DeathAbility : MonoBehaviour, IAbility
{
    public float damageRadius = 5f;
    public float damageInterval = 1f;
    public int damageAmount = 1;

    private bool isActive = false;
    private Coroutine damageCoroutine;

    private GameObject currentUser;

    public void Activate(GameObject user)
    {
        bool isUserPlayer = user.CompareTag("Player");

        if (isUserPlayer)
        {
            // Player može da toggle-uje
            if (!isActive)
            {
                currentUser = user;
                damageCoroutine = StartCoroutine(ApplyAoEDamage(user));
                isActive = true;
            }
            else
            {
                Stop();
            }
        }
        else
        {
            // Neprijatelji mogu samo da pokrenu ako nije aktivno
            if (!isActive)
            {
                currentUser = user;
                damageCoroutine = StartCoroutine(ApplyAoEDamage(user));
                isActive = true;
            }
        }
    }


    public void Stop()
    {
        if (isActive && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
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
                if (hit.gameObject == user)
                    continue;

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
}
