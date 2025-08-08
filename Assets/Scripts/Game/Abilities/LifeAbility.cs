using UnityEngine;

public class LifeAbility : MonoBehaviour, IAbility
{
    public int healAmount = 2;
    private int manaCost = 10;

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

            // Prvo proveravamo da li ima dovoljno mane
            if (!stats.UseMana(manaCost))
            {
                Debug.Log("Not enough mana for Life Ability.");
                return;
            }

            Debug.Log($"{user.name} activated LIFE ability!");
            stats.Heal(healAmount);
        }
        else
        {
            HealthBarBehaviour health = user.GetComponent<HealthBarBehaviour>();
            EnemyBehaviour enemy = user.GetComponent<EnemyBehaviour>();

            enemy.health += healAmount;
            Debug.Log($"Posle heala: {enemy.health}");

            if (enemy.health > enemy.maxHealth)
                enemy.health = enemy.maxHealth;

            health.SetHealth(enemy.health, enemy.maxHealth);
        }
    }
}