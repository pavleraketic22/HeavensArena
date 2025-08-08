using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{

    [Header("Death")]
    public Image deathImage;
    public float deathCooldown = 5f;
    private bool isCooldown = false;
    public KeyCode death;
    
    // Start is called before the first frame update
    void Start()
    {
        deathImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }

    void Death()
    {
        if (Input.GetKey(death) && isCooldown == false)
        {
            isCooldown = true;
            deathImage.fillAmount = 1;
        }

        if (isCooldown)
        {
            deathImage.fillAmount -= 1 / deathCooldown * Time.deltaTime;

            if (deathImage.fillAmount <= 0)
            {
                deathImage.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}