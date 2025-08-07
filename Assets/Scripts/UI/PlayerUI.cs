// HealthUI.cs
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, IHealthObserver
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image[] gems;
     

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

    public void OnGemAdded()
    {
        for (int i = 0; i < gems.Length; i++)
        {
            if (i < playerHealth.CurrentGems)
            {
                gems[i].color = Color.white;
            }
        } 
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.UnregisterObserver(this);
    }
}