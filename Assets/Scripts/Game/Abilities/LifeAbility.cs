using UnityEngine;

public class LifeAbility : MonoBehaviour, IAbility
{
    public int healAmount = 2;
    public HealthBarBehaviour HealthBarBehaviour;

    public void Activate(GameObject user)
    {
        if (user.gameObject.CompareTag("Player"))
        {
            Debug.Log($"{user.name} activated LIFE ability!");
            Stats stats = user.GetComponent<Stats>();
            if (stats != null)
            {
                stats.Heal(healAmount);
            }
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