using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTest : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;

    public int armor;

    public HealthBar healthBar;

    public int iFrameMax;
    public int iFrames;

    void Start()
    {
        curHealth = maxHealth;

        iFrames = iFrameMax;
    }

    void FixedUpdate()
    {
        if (iFrames > 0)
        {
            iFrames--;
        }
    }

    public void DealDamage(int damage)
    {
        if (iFrames == 0)
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

            iFrames += iFrameMax;
        }
    }
}