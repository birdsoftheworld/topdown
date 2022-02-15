using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int bulletDamage;
    public Faction bulletFaction;


    public float bulletSpeed;
    private Rigidbody2D rb2D;

    public float click;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

        click = 5; //40 - bulletSpeed;
    }

    void FixedUpdate()
    {
        rb2D.velocity = Vector3.zero;
        rb2D.AddForce(transform.up * bulletSpeed * -100f);

        click--;
        if (click < 0)
        {
            this.gameObject.GetComponent<Hittable>().safe = false;
            click = Mathf.Infinity;
        }
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
                if (hitted.CanHit(bulletFaction) == true || this.gameObject.GetComponent<Hittable>().safe == false)
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
