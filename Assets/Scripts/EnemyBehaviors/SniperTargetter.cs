using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTargetter : MonoBehaviour
{

    //public int bulletDamage;
    public Faction bulletFaction;


    public float bulletSpeed;
    private Rigidbody2D rb2D;

    public GameObject sniper;

    public float click = 10;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2D.velocity = Vector3.zero;
        rb2D.AddForce(transform.up * bulletSpeed * -50f);

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
            if (hitted.CanHit(bulletFaction) == true || this.gameObject.GetComponent<Hittable>().safe == false)
            {
                Destroy(gameObject);

                if (hitted.gameObject.tag != "Wall")
                {
                    sniper.GetComponent<sniperBehavior>().shotTarget = hitted.transform;
                    sniper.GetComponent<sniperBehavior>().takingShot = true;
                }
            }
        }
        

    }




}
