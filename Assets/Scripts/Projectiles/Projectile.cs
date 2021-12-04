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


        if (bulletFaction == (Faction)0)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 currentPos = this.transform.position;

            destination = destination - currentPos;

            Vector3 destinationN = destination.normalized;

            float angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, angle + 90);

            transform.rotation = rotation;
        }

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
