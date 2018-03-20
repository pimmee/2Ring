using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    public const float maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")]
    public float currentHealth = maxHealth;
    public RectTransform healthBar;

    public void TakeDamage(float amount)
    {
        if (!isServer)
            return;
        currentHealth -= amount;
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
   }

    void OnChangeHealth(float health) {

        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    void Die()
    {
        Debug.Log("Dead");
    }
}
