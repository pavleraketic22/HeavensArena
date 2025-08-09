using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class CooldownUI : MonoBehaviour
{
    private PlayerAttack cooldowns;
    
    public Image deathImage;
    public float deathCooldown;
    private bool isDeathCooldown = false;

    public Image lifeImage;
    public float lifeCooldown;
    private bool isLifeCooldown = false;

    public Image timeImage;
    public float timeCooldown;
    private bool isTimeCooldown = false;

    public Image fireballImage;
    public float fireballCooldown;
    private bool isFireballCooldown = false;
    private float timer;

    void Start()
    {
        cooldowns = FindObjectOfType<PlayerAttack>();

        deathCooldown = cooldowns.deathCooldown;
        lifeCooldown = cooldowns.lifeCooldown;
        timeCooldown = cooldowns.timeCooldown;
        fireballCooldown = cooldowns.fireballCooldown;
        timer = cooldowns.fireballCooldownTimer;

        deathImage.fillAmount = 0;
        lifeImage.fillAmount = 0;
        timeImage.fillAmount = 0;
        fireballImage.fillAmount = 0;
    }
    
    void Update()
    {
        Death();
        Life();
        TimeAbility();
        Fireball();
    }

    void Death()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isDeathCooldown && cooldowns.deathActivated)
        {
            isDeathCooldown = true;
            deathImage.fillAmount = 1;
        }

        if (isDeathCooldown)
        {
            deathImage.fillAmount -= 1 / deathCooldown * Time.deltaTime;

            if (deathImage.fillAmount <= 0)
            {
                deathImage.fillAmount = 0;
                isDeathCooldown = false;
            }
        }
    }

    void Life()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isLifeCooldown && cooldowns.lifeActivated)
        {
            isLifeCooldown = true;
            lifeImage.fillAmount = 1;
        }

        if (isLifeCooldown)
        {
            lifeImage.fillAmount -= 1 / lifeCooldown * Time.deltaTime;

            if (lifeImage.fillAmount <= 0)
            {
                lifeImage.fillAmount = 0;
                isLifeCooldown = false;
            }
        }
    }

    void TimeAbility()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTimeCooldown && cooldowns.timeActivated)
        {
            isTimeCooldown = true;
            timeImage.fillAmount = 1;
        }

        if (isTimeCooldown)
        {
            timeImage.fillAmount -= 1 / timeCooldown * Time.deltaTime;

            if (timeImage.fillAmount <= 0)
            {
                timeImage.fillAmount = 0;
                isTimeCooldown = false;
            }
        }
    }

    void Fireball()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isFireballCooldown && cooldowns.fireballActivated)
        {
            isFireballCooldown = true;
            fireballImage.fillAmount = 1;
        }

        if (isFireballCooldown)
        {
            fireballImage.fillAmount -= 1f / fireballCooldown * Time.deltaTime ;

            if (fireballImage.fillAmount <= 0)
            {
                fireballImage.fillAmount = 0;
                isFireballCooldown = false;
            }
        }
    }
}
*/

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
