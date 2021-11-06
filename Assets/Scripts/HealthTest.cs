using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTest : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;

    public int armor;

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

        //Debug.Log(armor);
        //Debug.Log(damage);
        //Debug.Log(hurt);

        if (hurt < 0)
        {
            hurt = 0;
        }
        //Debug.Log(hurt);
        curHealth -= hurt;
        if (healthBar != null)
        {
            //Debug.Log(this.gameObject);
            healthBar.SetHealth(curHealth);
        }
    }
}