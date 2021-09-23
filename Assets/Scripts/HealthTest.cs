using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTest : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;

    public int armor = 0;

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
        int hurt = damage - armor;
        if (hurt <= 0)
        {
            hurt = 1;
        }

        curHealth -= hurt;
        healthBar.SetHealth(curHealth);
    }
}