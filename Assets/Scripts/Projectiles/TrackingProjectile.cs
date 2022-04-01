using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : MonoBehaviour
{

    public int bulletDamage;
    public Faction bulletFaction;


    public float bulletSpeed;
    private Rigidbody2D rb2D;

    public float click;

    public GameObject trackDot;

    public GameObject trackerPrefab;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

        click = 5; //40 - bulletSpeed;

        trackDot = Instantiate(trackerPrefab, this.transform.position, this.transform.rotation);
    }

    void FixedUpdate()
    {
        rb2D.velocity = Vector3.zero;
        rb2D.AddForce(transform.up * bulletSpeed * -100f);

        Vector2 currentPos = this.transform.position;
        Vector2 targetPos = trackDot.transform.position;
        Vector2 destination = targetPos - currentPos;
        float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);
        transform.rotation = rotation;

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

                    trackDot.GetComponent<TrackingDot>().kill();

                    HealthTest health = coll.GetComponent<Collider2D>().GetComponent<HealthTest>();
                    if (health != null)
                    {
                        health.DealDamage(bulletDamage, this.transform.position);
                    }
                }
            }
        }
    }
}
