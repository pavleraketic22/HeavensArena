using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CooldownUI : MonoBehaviour
{
    private PlayerAttack cooldowns;

    public Image deathImage;
    public Image lifeImage;
    public Image timeImage;
    public Image fireballImage;

    void Start()
    {
        cooldowns = FindObjectOfType<PlayerAttack>();

        deathImage.fillAmount = 0;
        lifeImage.fillAmount = 0;
        timeImage.fillAmount = 0;
        fireballImage.fillAmount = 0;
    }

    void Update()
    {
        UpdateCooldownUI(ref cooldowns.deathCooldownTimer, cooldowns.deathCooldown, deathImage);
        UpdateCooldownUI(ref cooldowns.lifeCooldownTimer, cooldowns.lifeCooldown, lifeImage);
        UpdateCooldownUI(ref cooldowns.timeCooldownTimer, cooldowns.timeCooldown, timeImage);
        UpdateCooldownUI(ref cooldowns.fireballCooldownTimer, cooldowns.fireballCooldown, fireballImage);
    }

    private void UpdateCooldownUI(ref float currentTimer, float maxCooldown, Image image)
    {
        if (currentTimer > 0)
        {
            image.fillAmount = currentTimer / maxCooldown;
        }
        else
        {
            image.fillAmount = 0;
        }
    }
}
