using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBehavior : MonoBehaviour
{

    public HealthTest thisHealth;

    public HealthTest bodyHealth;

    //public Transform bulletPrefab;

    /*void Start()
    {
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
    }*/

    // Update is called once per frame
    void Update()
    {
        if (thisHealth.curHealth < 0)
        {
            //Debug.Log(thisHealth.curHealth);
            //Debug.Log(thisHealth.curHealth);
            //bodyHealth.curHealth += thisHealth.curHealth;
            bodyHealth.DealDamage((-1 * thisHealth.curHealth) + bodyHealth.armor);
            //Debug.Log("deal to t " + ((-1 * thisHealth.curHealth) + bodyHealth.armor));
            thisHealth.curHealth = 0;
        }

        if (thisHealth.armor <= 0)
        {
            Destroy(gameObject);
        }
    }
}
