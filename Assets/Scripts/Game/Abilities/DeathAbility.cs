using UnityEngine;

public class DeathAbility : MonoBehaviour, IAbility
{
    public int damage = 50;

    public void Activate(GameObject user)
    {
        Debug.Log($"{user.name} activated DEATH ability!");

        Collider2D[] enemies = Physics2D.OverlapCircleAll(user.transform.position, 5f);
        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Health health = enemy.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }
}