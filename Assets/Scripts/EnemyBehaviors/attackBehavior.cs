using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackBehavior : MonoBehaviour
{

    private GameObject player;

    private Vector2 destination;

    //public HealthTest health;

    public float distance;

    public bool active = false;

    public float speed;
    private Rigidbody2D rb2D;

    public GameObject target;

    public Transform targetL;

    public int idleNumber;

    private int waiting;

    public string behavior = "none";


    public float fireSpeed;
    private float fireTick = 0;

    public float bulletSpeed;
    public int bulletDamage;
    public Faction bulletFaction;
    public GameObject bulletPrefab;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb2D = GetComponent<Rigidbody2D>();

        target = player;

        targetL = target.transform;

        waiting = 0;

        fireTick = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (health.curHealth <= 0)
        //{
        //    Destroy(gameObject);
        //}

        ////////  distance to activate
        float distance2 = Vector3.Distance(this.transform.position, player.transform.position);

        if (distance2 <= distance)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, player.transform.position - this.transform.position);
            if (hit.collider.gameObject.tag == "Player")
            {
                //Debug.Log("We found Target!");
                active = true;
                onActivate();
                //targetL = player.transform;
            }
            else
            {
                //Debug.Log("I found something else with name = " + hit.collider.name);
            }
        }

        if (active == true && distance2 >= distance * 3)
        {
            active = false;
            onDeactivate();
        }
    }

    void FixedUpdate()
    {
        rb2D.velocity = rb2D.velocity / 3;

        if (waiting > 0)
        {
            waiting--;
        }
        else
        {
            if (behavior == "none")
            {
                if (Vector2.Distance(this.transform.position, findCover().transform.position) > Vector2.Distance(this.transform.position, player.transform.position))
                {
                    behavior = "shoot";
                }

                else if (Random.Range(0, 2) == 0)
                {
                    behavior = "shoot";
                    targetL = player.transform;
                    FaceTarget(targetL.position);
                }
                else
                {
                    behavior = "run";
                }

            }
            else if (behavior == "run")
            {
                if (targetL.tag != "Cover") { targetL = findCover().transform; }

                FaceTarget(targetL.position);

                if (this.GetComponent<Collider2D>().Distance(targetL.GetComponent<Collider2D>()).distance < 1.1)
                {
                    behavior = "shoot";
                }
                else
                {
                    FaceTarget(targetL.position);
                    rb2D.AddForce(transform.up * speed * -100f);
                }
            }
            else if (behavior == "shoot")
            {
                targetL = player.transform;
                FaceTarget(targetL.position);

                if (Vector3.Distance(this.transform.position, player.transform.position) >= (distance * 2.5))
                {
                    rb2D.AddForce(transform.up * speed * -100f);

                    if (checkShot() == true)
                    {
                        if (fireTick >= fireSpeed)
                        {
                            waiting = waiting + 30;
                            shoot(30);
                            behavior = "run";
                            fireTick = 0;
                        }
                        else
                        {
                            fireTick++;
                        }
                    }
                }
                else if (Vector3.Distance(this.transform.position, player.transform.position) <= distance)
                {
                    rb2D.AddForce(transform.up * speed * 50f);

                    if (checkShot() == true)
                    {

                        if (fireTick >= fireSpeed)
                        {
                            waiting = waiting + 10;
                            shoot(45);
                            behavior = "run";
                            fireTick = 0;
                        }
                        else
                        {
                            fireTick++;
                        }
                    }

                }
                else
                {
                    if (checkShot() == true)
                    {

                        if (fireTick >= fireSpeed)
                        {
                            shoot(15);
                            behavior = "run";
                            fireTick = 0;
                        }
                        else
                        {
                            fireTick++;
                            fireTick++;
                        }
                    }
                }
            }
        }
    }


    private void FaceTarget(Vector2 targetPos)
    {
        Vector2 currentPos = this.transform.position;
        destination = targetPos - currentPos;
        float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);
        transform.rotation = rotation;
    }

    private void onActivate()
    {

    }

    private void onDeactivate()
    {
        waiting = waiting + 30;
    }

    private void shoot(int shake)
    {
        this.transform.Rotate(new Vector3(0, 0, Random.Range((-1 * shake), (shake + 1))));
        if (Mathf.Round(this.transform.eulerAngles.z) == 180) { this.transform.Rotate(new Vector3(0, 0, -180)); }


        Vector2 position = this.transform.position;
        GameObject clone = Instantiate(bulletPrefab, position, this.transform.rotation);
        clone.gameObject.SetActive(true);

        Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

        bullet.bulletSpeed = bulletSpeed;
        bullet.bulletFaction = bulletFaction;
        bullet.bulletDamage = bulletDamage;




    }

    public GameObject findCover()
    {
        //find all cover
        GameObject[] cover;


        cover = GameObject.FindGameObjectsWithTag("Cover");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in cover)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public bool checkShot()
    {
        bool clearShot = false;

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, player.transform.position - this.transform.position);
        if (hit.collider.gameObject.tag == "Player")
        {
            clearShot = true;
        }
        else
        {
            clearShot = false;
        }

        return clearShot;

    }

}
