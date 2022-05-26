using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDamage : MonoBehaviour
{
    public int bulletDamage;
    private Rigidbody2D rb2D;
    private int ticker;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        ticker = 0;
    }

    void FixedUpdate()
    {
        ticker++;
        if (ticker == 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.GetComponent<HealthTest>() != null)
        {
            coll.GetComponent<HealthTest>().DealDamage(bulletDamage, this.transform.position);
            Destroy(gameObject);
        }
    }

}
