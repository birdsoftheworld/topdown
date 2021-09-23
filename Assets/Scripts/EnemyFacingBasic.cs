using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFacingBasic : MonoBehaviour
{

    public GameObject player;

    private Vector2 destination;

    public float fireSpeed;
    private float fireTick = 0;

    public HealthTest health;

    public Rigidbody2D bullet;

    public float bulletSpeed;
    public int bulletDamage;
    public Faction bulletFaction;

    public GameObject bulletPrefab;

    public float distance;


    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        fireTick = 0;

        player = GameObject.FindGameObjectWithTag("Player");
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


        if (health.curHealth <= 0)
        {
            Destroy(gameObject);

        }



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
        }

    }






    void FixedUpdate()
    {
        if (active == true) { fireTick++; }
        if (fireTick == fireSpeed)
        {
            Fire();

            fireTick = 0;
        }
    }


    private void Fire()
    {
        Vector2 position = this.transform.position;
        GameObject clone = Instantiate(bulletPrefab, position, this.transform.rotation);
        clone.gameObject.SetActive(true);

        Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

        bullet.bulletSpeed = bulletSpeed;
        bullet.bulletFaction = bulletFaction;
        bullet.bulletDamage = bulletDamage;
    }
}
