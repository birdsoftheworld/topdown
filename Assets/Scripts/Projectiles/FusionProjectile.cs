using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionProjectile : MonoBehaviour
{
    private SpriteRenderer sprite;
    public int bulletDamage;
    public Faction bulletFaction;

    public GameObject bombPrefab;

    public float bulletSpeed;
    private Rigidbody2D rb2D;

    private int counter;

    private int counterTotal;


    void Awake()
    {
        counter = 0;

        counterTotal = 0;

        rb2D = GetComponent<Rigidbody2D>();

        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        rb2D.velocity = Vector3.zero;
        rb2D.AddForce(transform.up * bulletSpeed * -100f);

        if (counter > 9)
        {
            Vector2 position = this.transform.position;
            GameObject clone = Instantiate(bombPrefab, position, this.transform.rotation);
            clone.gameObject.SetActive(true);

            RichochetBomb grenade = clone.gameObject.GetComponent("RichochetBomb") as RichochetBomb;

            grenade.bulletSpeed = bulletSpeed * 2;
            grenade.countdownMax = 75;
            grenade.countdown = 75;
            grenade.damage = 1;
            grenade.primeTime = 1;

            clone.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

            counter = 0;
            counterTotal++;
            this.transform.localScale += new Vector3(-0.005f, -0.03f, 0f);
        }

        counter++;

        if (counterTotal > 10)
        {
            Destroy(gameObject);
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
                if (hitted.CanHit(bulletFaction))
                {
                    HealthTest health = coll.GetComponent<Collider2D>().GetComponent<HealthTest>();
                    if (health != null)
                    {
                        health.DealDamage(bulletDamage + health.armor);
                    }

                    counter += 5;
                    counterTotal++;
                    this.transform.localScale += new Vector3(-0.005f, -0.03f, 0f);
                }
            }
        }
    }
}
