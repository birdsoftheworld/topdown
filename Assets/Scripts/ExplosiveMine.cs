using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMine : MonoBehaviour
{

    public HealthTest health;

    public GameObject explosionPrefab;
    public int damage;
    public int size;
    public int push;

    public int explodeTimerish;
    private int upTick;

    // Start is called before the first frame update
    void Start()
    {
        //rb2D = GetComponent<Rigidbody2D>();

        health = this.gameObject.GetComponent<HealthTest>();

        upTick = 0;
    }

    /*void FixedUpdate()
    {
        rb2D.velocity = rb2D.velocity / 4;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (health.curHealth <= 0)
        {

            if (upTick >= explodeTimerish)
            {

                GameObject clone = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
                clone.gameObject.SetActive(true);

                Explosion explosion = clone.gameObject.GetComponent("Explosion") as Explosion;

                explosion.damage = damage;
                explosion.startSpeed = push;
                explosion.explosionSize = size;

                clone.gameObject.SetActive(true);

                Destroy(gameObject);
            }
            else
            {
                upTick += 1;
            }
        }
    }



    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Hittable>() != null)
        {
            if (coll.gameObject.tag != "Projectile")
            {
                health.curHealth = 0;
            }
        }
    }
}
