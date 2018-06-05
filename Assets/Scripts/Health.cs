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

    private NetworkStartPosition[] spawnPoints;

    void Start() {
        if (isLocalPlayer) {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    public void TakeDamage(float amount)
    {
        //if (!isServer)
        //    return;
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            RpcRespawn();
            //Die();
        }
   }

    void OnChangeHealth(float health) {
        print(health);
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn() {
        if (isLocalPlayer) {
            // Set the spawn point to origin as default value
            Vector3 spawnPoint = Vector3.zero;
            // If there is a spawn point array and the array is not empty, pick one at random
            if (spawnPoints != null && spawnPoints.Length > 0) {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }
            transform.position = spawnPoint;
        }
    }

    void Die()
    {
        Debug.Log("Dead");
    }
}
