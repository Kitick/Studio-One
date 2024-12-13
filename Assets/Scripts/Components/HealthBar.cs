using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image HealthBarSprite;
    public void UpdateHealthBar(int maxHealth, int currentHealth)
    {
        HealthBarSprite.fillAmount = (float)currentHealth / maxHealth;
    }
}