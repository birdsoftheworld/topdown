using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float bulletDamage;
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

    void Update()
    {
        rb2D.velocity = Vector3.zero;
        rb2D.AddForce(transform.up * bulletSpeed * -1f);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {

        Hittable hitted = coll.GetComponent<Collider2D>().GetComponent<Hittable>();

        Faction hitFact = hitted.faction;

        Debug.Log("hit" + hitFact);
        Debug.Log("me" + bulletFaction);

        if (hitted != null)
        {
           if (hitted.CanHit(bulletFaction))
           {
                Debug.Log("true");
                this.gameObject.SetActive(false);


            }
           else
           {
               Debug.Log("false");
           }
        }



        //bulletSpeed = bulletSpeed * -1;

    }


    
}
