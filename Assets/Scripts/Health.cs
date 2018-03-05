using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float hitPoints = 100f;
    float currentHitPoints;

    void Start()
    {
        currentHitPoints = hitPoints;
    }


    public void TakeDamage(float amt)
    {
        currentHitPoints -= amt;
        Debug.Log(currentHitPoints);

        if (currentHitPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }
}
