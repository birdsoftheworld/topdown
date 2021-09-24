using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserScript : MonoBehaviour
{

    public GameObject player;

    private Vector2 destination;

    public float tickSpeed = 24;
    private float chargeTick = 6;

    //public HealthTest health;

    //public Rigidbody2D bullet;

    //public float bulletSpeed;
    //public int bulletDamage;
    //public Faction bulletFaction;

    //public GameObject bulletPrefab;

    public float distance;


    public bool active = false;

    public float speed;
    private Rigidbody2D rb2D;

    public int decel;
    private int currentDecel;


    // Start is called before the first frame update
    void Start()
    {
        //fireTick = 0;

        player = GameObject.FindGameObjectWithTag("Player");

        rb2D = GetComponent<Rigidbody2D>();

        currentDecel = decel;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = player.transform.position;


        Vector2 currentPos = this.transform.position;

        destination = playerPos - currentPos;


        float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);

        transform.rotation = rotation;


        //if (health.curHealth <= 0)
        //{
        //    Destroy(gameObject);

        //}



        ////////  distance to activate
        float distance2 = Vector3.Distance(this.transform.position, playerPos);

        if (distance2 <= distance)
        {

            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, player.transform.position - this.transform.position);
            if (hit.collider.gameObject.tag == "Player")
            {
                     Debug.Log("We found Target!");

                active = true;
            }
            else
            {
                  Debug.Log("I found something else with name = " + hit.collider.name);
            }
        }

        if (distance2 >= distance * 3)
        {
            active = false;
            chargeTick = tickSpeed / 4;
        }

        if (active == true)
        {
            rb2D.velocity = rb2D.velocity / currentDecel;
            rb2D.AddForce(transform.up * speed * -1f);



        }
        else
        {
            rb2D.velocity = rb2D.velocity / currentDecel;

        }

        if (currentDecel > decel)
        {
            currentDecel--;
        }

    }






    void FixedUpdate()
    {
        if (active == true) { chargeTick++; }
        if (chargeTick == tickSpeed)
        {
            Charge();

            chargeTick = 0;
        }
    }


    private void Charge()
    {
        Debug.Log("CHARGE!");
        rb2D.AddForce(transform.up * speed * -50f);
        currentDecel = decel - 3;

    }

    /*private void Fire()
    {
        Vector2 position = this.transform.position;
        GameObject clone = Instantiate(bulletPrefab, position, this.transform.rotation);
        clone.gameObject.SetActive(true);

        Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

        bullet.bulletSpeed = bulletSpeed;
        bullet.bulletFaction = bulletFaction;
        bullet.bulletDamage = bulletDamage;
    }*/
}
