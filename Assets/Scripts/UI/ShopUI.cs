using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Stats playerStats;

    public Button healthUpgradeButton;
    public Button manaUpgradeButton;

    public TextMeshProUGUI healthUpgradeCostText;
    public TextMeshProUGUI manaUpgradeCostText;

    public TextMeshProUGUI feedbackText; 

    private void Start()
    {
        if (playerStats == null)
        {
            Debug.LogError("ShopUI: Player Stats nije dodeljen!");
            return;
        }

        UpdateUI();

        healthUpgradeButton.onClick.AddListener(() => BuyHealthUpgrade());
        manaUpgradeButton.onClick.AddListener(() => BuyManaUpgrade());
    }

    private void UpdateUI()
    {
        healthUpgradeCostText.text = $"Cost: {playerStats.healthUpgradeCost} Coins";
        manaUpgradeCostText.text = $"Cost: {playerStats.manaUpgradeCost} Coins";
        
        
    }

    public void BuyHealthUpgrade()
    {
        bool success = playerStats.BuyHealthUpgrade();
        ShowFeedback(success, "Health upgraded!", "Not enough coins for health upgrade!");
        UpdateUI();
    }

    public void BuyManaUpgrade()
    {
        bool success = playerStats.BuyManaUpgrade();
        ShowFeedback(success, "Mana upgraded!", "Not enough coins for mana upgrade!");
        UpdateUI();
    }
    

    private void ShowFeedback(bool success, string successMessage, string failMessage)
    {
        feedbackText.text = success ? successMessage : failMessage;
        CancelInvoke(nameof(ClearFeedback));
        Invoke(nameof(ClearFeedback), 2f); 
    }

    private void ClearFeedback()
    {
        feedbackText.text = "";
    }
}
