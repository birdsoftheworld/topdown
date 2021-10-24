using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashbang : MonoBehaviour
{
    public Faction bulletFaction;


    public float bulletSpeed;
    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;

    public int countdownMax;
    public int countdown;

    private bool exploding = false;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        this.GetComponent<Collider2D>().isTrigger = true;

        countdown = countdownMax;

        exploding = false;
    }

    void FixedUpdate()
    {
        if (countdown == (countdownMax - 3))
        {
            rb2D.AddForce(transform.up * bulletSpeed * -75f);
        }
        if (countdown == (countdownMax - 4))
        {
            this.GetComponent<Collider2D>().isTrigger = false;
        }

        if (countdown > 0)
        {
            countdown--;
        }
        else
        {
            if (countdown != (countdownMax / -10)) 
            { 
                countdown--;
                rb2D.velocity = Vector3.zero;
                this.GetComponent<Collider2D>().isTrigger = true;
                sprite.color = Color.white;
            }

            else
            {
                exploding = true;

                if (this.transform.localScale != new Vector3(6.2f, 6.2f, 7f))
                {
                    this.transform.localScale += new Vector3(2f, 2f, 2f);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            //if (sprite.size == )
        }


    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (exploding == true)
        {
            if (coll.GetComponent<Collider2D>().GetComponent<Hittable>() != null)
            {
                if (coll.GetComponent<pistoleerBehavior>() != null)
                {
                    coll.GetComponent<pistoleerBehavior>().waiting = 100;
                }
                if (coll.GetComponent<PlayerCarbine>() != null)
                {
                    coll.GetComponent<PlayerCarbine>().waiting = 75;
                }
                if (coll.GetComponent<PlayerRifle>() != null)
                {
                    coll.GetComponent<PlayerRifle>().waiting = 75;
                }
                if (coll.GetComponent<Player>() != null)
                {
                    coll.GetComponent<Player>().waiting = 75;
                }

                Hittable hitted = coll.GetComponent<Collider2D>().GetComponent<Hittable>();

                Faction hitFact = hitted.faction;
                if (hitted != null)
                {
                    HealthTest health = coll.GetComponent<Collider2D>().GetComponent<HealthTest>();
                    if (health != null)
                    {
                        health.DealDamage(1);
                    }
                }
            }
        }
    }
}