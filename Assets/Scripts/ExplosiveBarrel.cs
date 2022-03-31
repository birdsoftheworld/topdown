using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{

    public HealthTest health;
    private Rigidbody2D rb2D;

    public GameObject explosionPrefab;
    public int damage;
    public int size;
    public int push;

    public int explodeTimerish;
    private int upTick;
    public int upBy;

    public Sprite im0;
    public Sprite im1;

    public SpriteRenderer renderer;

    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

        health = this.gameObject.GetComponent<HealthTest>();

        upTick = 0;

        renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        rb2D.velocity = rb2D.velocity / 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.justHalved == true)
        {
            renderer.sprite = im1;

            Vector2 currentPos = this.transform.position;
            Vector2 destination = health.injuredFrom - currentPos;
            float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, angle - 90);
            transform.rotation = rotation;

            rb2D.AddForce(transform.up * -50f);

            this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();

            health.justHalved = false;
        }

        if (health.curHealth <= 0)
        {
            //this.GetComponent<SpriteRenderer>().color = Color.red;



            //rb2D.velocity = Vector3.zero;
            rb2D.AddForce(transform.up * upTick * -10f);
            this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();

            upTick -= health.curHealth * 10;
            health.curHealth = 0;

            if (upTick >= explodeTimerish)
            {

                GameObject clone = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
                clone.gameObject.SetActive(true);

                Explosion explosion = clone.gameObject.GetComponent("Explosion") as Explosion;

                explosion.damage = damage;
                explosion.startSpeed = push;
                explosion.explosionSize = size;

                //explosion.originalSize = clone.transform.localScale.x;

                //explosion.targetSize = explosion.originalSize + size;


                clone.gameObject.SetActive(true);

                Destroy(gameObject);
            }
            else
            {
                upTick += Random.Range(0, upBy);
            }
        }
    }



    /*void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            Vector2 currentPos = this.transform.position;
            Vector2 targetPos = coll.transform.position;
            Vector2 destination = targetPos - currentPos;
            float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, angle + 90);
            transform.rotation = rotation;
        }
    }*/
}
