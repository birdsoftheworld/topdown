using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int bulletDamage;
    public Faction bulletFaction;


    public float bulletSpeed;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();


    }

    void FixedUpdate()
    {
        rb2D.velocity = Vector3.zero;
        rb2D.AddForce(transform.up * bulletSpeed * -100f);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Collider2D>().GetComponent<Hittable>() != null)
        {
            Hittable hitted = coll.GetComponent<Collider2D>().GetComponent<Hittable>();
            Faction hitFact = hitted.faction;

            if (coll.GetComponent<Collider2D>().isTrigger == true)
            {

            }
            else if (hitted != null)
            {
                if (hitted.CanHit(bulletFaction))
                {
                    Destroy(gameObject);
                    HealthTest health = coll.GetComponent<Collider2D>().GetComponent<HealthTest>();
                    if (health != null)
                    {
                        health.DealDamage(bulletDamage);
                    }
                }
            }
        }
    }


    
}
