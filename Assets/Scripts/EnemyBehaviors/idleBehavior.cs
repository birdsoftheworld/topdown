using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleBehavior : MonoBehaviour
{

    private GameObject player;

    private Vector2 destination;

    //public HealthTest health;

    public float distance;

    public bool active = false;

    public float speed;
    private Rigidbody2D rb2D;

    public GameObject target;

    public GameObject[] idleLocations;

    public Transform targetL;

    public int idleNumber;

    private int waiting;

    public string attackBehavior = "none";


    public float fireSpeed;
    private float fireTick = 0;

    public float bulletSpeed;
    public int bulletDamage;
    public Faction bulletFaction;
    public GameObject bulletPrefab;

    public bool colliding = false;
    public Collider2D coll;


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
            if (active == true)
            {
                if (attackBehavior == "none")
                {
                    if (Vector2.Distance(this.transform.position, findCover().transform.position) > Vector2.Distance(this.transform.position, player.transform.position)) {
                        attackBehavior = "shoot";
                    }

                    else if (Random.Range(0, 2) == 0)
                    {
                        attackBehavior = "shoot";
                        targetL = player.transform;
                        FaceTarget(targetL.position);
                    }
                    else
                    {
                        attackBehavior = "run";
                    }

                }
                else if (attackBehavior == "run")
                {
                    if (targetL.tag != "Cover") {targetL = findCover().transform; }

                    FaceTarget(targetL.position);

                    if (this.GetComponent<Collider2D>().Distance(targetL.GetComponent<Collider2D>()).distance < 1.1)
                    {
                        attackBehavior = "shoot";
                    }
                    else
                    {
                        FaceTarget(targetL.position);
                        /*if (colliding == true)
                        {
                            adjustForCollision();
                        }*/
                        rb2D.AddForce(transform.up * speed * -100f);
                    }
                }
                else if (attackBehavior == "shoot")
                {
                    targetL = player.transform;
                    FaceTarget(targetL.position);

                    if (Vector3.Distance(this.transform.position, player.transform.position) >= (distance * 2.5))
                    {
                        /*if (colliding == true)
                        {
                            adjustForCollision();
                        }*/
                        rb2D.AddForce(transform.up * speed * -100f);

                        if (checkShot() == true)
                        {
                            if (fireTick >= fireSpeed)
                            {
                                waiting = waiting + 30;
                                shoot(30);
                                attackBehavior = "run";
                                fireTick = 0;
                            }
                            else
                            {
                                fireTick++;
                            }
                        }
                    }
                    else if (Vector3.Distance(this.transform.position, player.transform.position) <= distance )
                    {
                        rb2D.AddForce(transform.up * speed * 50f);

                        if (checkShot() == true)
                        {

                            if (fireTick >= fireSpeed)
                            {
                                waiting = waiting + 10;
                                shoot(45);
                                attackBehavior = "run";
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
                                attackBehavior = "run";
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
            else
            {
                setIdleTarget();
                FaceTarget(targetL.position);
                /*if (colliding == true)
                {
                    adjustForCollision();
                }*/
                rb2D.AddForce(transform.up * speed * -75f);
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

    private void setIdleTarget()
    {
        if (targetL == player.transform)         //when lose player
        {
            //waiting = waiting + 20; already assigned in Deactivate
            float targetDistance = Vector3.Distance(this.transform.position, idleLocations[0].transform.position);
            targetL = idleLocations[0].transform;

            for (int i = 0; i < idleLocations.Length; i++)
            {
                float newDist = Vector3.Distance(this.transform.position, idleLocations[i].transform.position);

                if (targetDistance > newDist)
                {
                    targetDistance = newDist;
                    targetL = idleLocations[i].transform;
                    idleNumber = i;
                }
            }

        }
        else //when just retargeting upon reaching target
        {

            if (Vector2.Distance(this.transform.position, targetL.position) < 1.1)
            {
                idleNumber++;

                if (idleNumber == 3) { idleNumber = 0; }
                targetL = idleLocations[idleNumber].transform;

                waiting = waiting + 20;
            }
            else
            {

            }
        }
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

    public GameObject findCover() {
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

    /*private void adjustForCollision()
    {
        if (coll.CompareTag("Cover"))
        {
            this.transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90);
        }

        for (int i = 45; i == 360; i = i * -1)
        {
            this.transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z + i);

            if (this.GetComponent<Collider2D>().IsTouching(coll) == true)
            {
                i = 360;
            }

            else
            {
                i = Mathf.Abs(i) + 45 * (i/ Mathf.Abs(i));
            }

        }

        if (this.GetComponent<Collider2D>().IsTouching(coll) == false)
        {
            colliding = false;
            //Debug.Log("bonk");
        }
    }*/


        /*private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Cover"))
        {
            coll = collision;
            colliding = true;
        }

    
        //rb2D.AddForce(transform.up * speed * 25f);

    }*/

}