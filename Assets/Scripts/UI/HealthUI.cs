// HealthUI.cs
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour, IHealthObserver
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private Health playerHealth;

    private void Start()
    {
        if (playerHealth != null)
            playerHealth.RegisterObserver(this);
    }

    public void OnHealthChanged(int currentHealth, int maxHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.CurrentHealth)
            {
                hearts[i].color = Color.red;
            }else
            {
                hearts[i].color = Color.black;
            }
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.UnregisterObserver(this);
    }
}