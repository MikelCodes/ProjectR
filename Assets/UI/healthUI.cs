using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthUI : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private float maxHP;

    public void setHealth (float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(health-1/maxHP);
    }

    public void setMaxHealth (float health, float maxHealth)
    {
        slider.value = health;
        slider.maxValue = maxHealth;
        maxHP = maxHealth;

        fill.color = gradient.Evaluate(health/maxHP);
    }
}
