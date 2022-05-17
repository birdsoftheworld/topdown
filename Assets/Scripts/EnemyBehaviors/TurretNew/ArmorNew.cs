using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorNew : MonoBehaviour
{
    HealthTest myHealth;

    public HealthTest guardingHealth;

    // Start is called before the first frame update
    void Start()
    {
        myHealth = this.gameObject.GetComponent<HealthTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myHealth.curHealth < 0)
        {
            guardingHealth.DealDamage(-1 * myHealth.curHealth, myHealth.injuredFrom);

            myHealth.curHealth = myHealth.maxHealth;
        }
        else if (myHealth.curHealth < myHealth.maxHealth)
        {
            myHealth.curHealth++;
        }
    }
}
