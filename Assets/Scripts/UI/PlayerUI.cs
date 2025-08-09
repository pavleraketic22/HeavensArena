// HealthUI.cs
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, IStatsObserver
{
    [FormerlySerializedAs("playerHealth")] [SerializeField] private Stats playerStats;
    [SerializeField] private Image[] gems;
    public Slider healthSlider;
    public Slider manaSlider;
    

    public void OnHealthChanged(int currentHealth, int maxHealth)
    {
        healthSlider.value = (float)currentHealth / maxHealth;
    }

    public void OnManaChanged(int currentMana, int maxMana)
    {
        manaSlider.value = (float)currentMana / maxMana;
    }

    private void Start()
    {
        if (playerStats != null)
            playerStats.RegisterObserver(this);
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