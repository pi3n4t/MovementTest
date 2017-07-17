using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Creature {

    float maxHealth = 100;
    float currentHealth;

    private Image healthBar;
    private Text healthText;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.FindGameObjectWithTag(StringCollection.HEALTHBAR).GetComponent<Image>();
        healthText = GameObject.FindGameObjectWithTag(StringCollection.HEALTHTEXT).GetComponent<Text>();
        UpdateHealthBar();
    }

    public void ChangeHealth(float healthToAdd)
    {
        currentHealth = Globals.ChangeValue(healthToAdd, currentHealth, maxHealth);
        UpdateHealthBar();
        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    void UpdateHealthBar()
    {
        UpdateBar(healthBar, currentHealth, maxHealth);
        UpdateBarText(healthText, currentHealth, maxHealth);
    }
}
