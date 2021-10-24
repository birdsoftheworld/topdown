using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracer : MonoBehaviour
{

    //public int bulletDamage;
    public Faction bulletFaction;


    public float bulletSpeed;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();


        /*destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 destinationN = destination.normalized;

        float angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);

        transform.rotation = rotateZeroMark.rotation;

        transform.rotation = rotation;*/


        //     rb2D.AddForce(transform.up * bulletSpeed * -1f);


    }

    void FixedUpdate()
    {
        rb2D.velocity = Vector3.zero;
        rb2D.AddForce(transform.up * bulletSpeed * -50f);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Collider2D>().GetComponent<Hittable>() != null)
        {
            Hittable hitted = coll.GetComponent<Collider2D>().GetComponent<Hittable>();
            Faction hitFact = hitted.faction;

            if (hitted != null)
            {
                if (hitted.CanHit(bulletFaction))
                {
                    Destroy(gameObject);
                    if (coll.GetComponent<Collider2D>().GetComponent<HealthTest>() != null)
                    {
                        coll.GetComponent<Collider2D>().GetComponent<HealthTest>().armor--;
                        coll.GetComponent<Collider2D>().GetComponent<HealthTest>().armor--;
                    }
                    /*if (armor > -1)
                    {
                        health.armor;
                    }*/
                }
            }
        }
    }



}
