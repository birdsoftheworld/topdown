using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionSize;
    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;

    public float originalSize;

    public float targetSize;

    public int damage;

    public int startSpeed;

    private float increaseSpeed;

    private void Awake()
    {
        //explosionSize = 5;

        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        originalSize = this.transform.localScale.x;

        targetSize = originalSize + explosionSize;

        rb2D.AddForce(transform.up * startSpeed * -100f);

        increaseSpeed = explosionSize / 4;

        //Debug.Log(originalSize);
        //Debug.Log(explosionSize);

        //Debug.Log(targetSize);
    }

    void FixedUpdate()
    {
        targetSize = originalSize + explosionSize;
        increaseSpeed = explosionSize / 1;


        rb2D.velocity = rb2D.velocity / 2;
        //Debug.Log(this.transform.localScale.x);

        if (this.transform.localScale.x < targetSize)
        {
            //Debug.Log("-");
            this.transform.localScale += new Vector3(increaseSpeed, increaseSpeed, increaseSpeed);
            //Debug.Log(this.transform.localScale.x);
            //Debug.Log("-");

        }
        else
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
            if (hitted != null)
            {
                HealthTest health = coll.GetComponent<Collider2D>().GetComponent<HealthTest>();
                if (health != null)
                {
                    health.DealDamage(damage, this.transform.position);
                }
            }
        }
    }
    
}
