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

    private void Start()
    {
        fill.color = gradient.Evaluate(0.999f/1);
    }

    public void setHealth (float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(health/maxHP);
    }

    public void setMaxHealth (float health, float maxHealth)
    {
        slider.value = health;
        slider.maxValue = maxHealth;
        maxHP = maxHealth;

        fill.color = gradient.Evaluate(health/maxHP);
    }
}
