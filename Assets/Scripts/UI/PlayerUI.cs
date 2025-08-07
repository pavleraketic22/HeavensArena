// HealthUI.cs
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, IStatsObserver
{
    [SerializeField] private Image[] hearts;
    [FormerlySerializedAs("playerHealth")] [SerializeField] private Stats playerStats;
    [SerializeField] private Image[] gems;
     

    private void Start()
    {
        if (playerStats != null)
            playerStats.RegisterObserver(this);
    }

    public void OnHealthChanged(int currentHealth, int maxHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerStats.CurrentHealth)
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
            if (i < playerStats.CurrentGems)
            {
                gems[i].color = Color.white;
            }
        } 
    }

    private void OnDestroy()
    {
        if (playerStats != null)
            playerStats.UnregisterObserver(this);
    }
}