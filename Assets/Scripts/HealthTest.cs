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

    public bool justHalved;

    public bool justThirded;

    void Start()
    {
        justHalved = false;

        justThirded = false;

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

            if (curHealth - hurt <= maxHealth / 2)
            {
                justHalved = true;
            }

            if (curHealth - hurt <= maxHealth / 3)
            {
                justThirded = true;
            }

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