using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class neonBar : MonoBehaviour
{
    public Slider bar;
    public HealthTest playerHealth;

    public int drain;

    public int curHealth;

    public int neon;

    private int waiting;

    private void Start()
    {
        bar = GetComponent<Slider>();
        bar.maxValue = neon;
        bar.value = neon;

        this.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        drain = 0;

        curHealth = playerHealth.curHealth;

        waiting = 0;
    }

    void Update()
    {
        if (curHealth != playerHealth.curHealth)
        {
            if (curHealth > playerHealth.curHealth)
            {
                drain += (curHealth - playerHealth.curHealth) * (curHealth - playerHealth.curHealth) * 5;
            }
            else if (curHealth < playerHealth.curHealth)
            {
                drain = 0;
            }
            curHealth = playerHealth.curHealth;
        }
    }

    void FixedUpdate()
    {
        if (waiting > 0)
        {
            waiting--;
        }
        else if (drain > 0)
        {
            drain--;
            neon--;
            bar.value = neon;

            waiting = 5;
        }
        
        if (neon < 1)
        {
            playerHealth.curHealth -= 1;
            neon += 20;
            bar.value = neon;
            playerHealth.healthBar.SetHealth(playerHealth.curHealth);
        }
    }
}