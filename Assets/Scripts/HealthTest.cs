using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTest : MonoBehaviour
{
    public int curHealth = 0;
    public int maxHealth = 100;

    public HealthBar healthBar;

    void Start()
    {
        curHealth = maxHealth;
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DealDamage(10);
        }
    }*/

    public void DealDamage(int damage)
    {
        curHealth -= damage;
        healthBar.SetHealth(100 * curHealth / maxHealth);
    }
}