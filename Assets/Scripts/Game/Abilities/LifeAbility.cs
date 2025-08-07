using UnityEngine;

public class LifeAbility : MonoBehaviour, IAbility
{
    public int healAmount = 20;

    public void Activate(GameObject user)
    {
        Debug.Log($"{user.name} activated LIFE ability!");
        Health health = user.GetComponent<Health>();
        if (health != null)
        {
            health.Heal(healAmount);
        }
    }
}