using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarBehaviour : MonoBehaviour
{

    public Slider slider;

    public Color low;

    public Color high;

    public Vector3 offset;


    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;
        float fillAmount = slider.value / slider.maxValue;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, fillAmount);


    }
    void Update()
    {
        slider.transform.position = UnityEngine.Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
