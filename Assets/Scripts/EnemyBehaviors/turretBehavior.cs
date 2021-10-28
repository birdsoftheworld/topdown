using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBehavior : MonoBehaviour
{

    private GameObject player;

    public HealthTest health;

    public float distance;

    public bool active = false;

    public float speed;
    private Rigidbody2D rb2D;

    public List<GameObject> nearbyOtherRooms;

    public List<float> swivels;

    public int waiting;
    public int scanning = 0;

    public int location;

    public Collider2D locationCol;
    private GameObject levelGen;


    public int bulletSpeed;
    public int bulletDamage;
    public GameObject bulletPrefab;

    private Transform chasingTarget;

    public int facingNumber = 0;

    public Transform facingTarge;

    public PolygonCollider2D cone;

    private bool isShooting = false;

    private bool sensingPlayer = false;

    public int killCounterMax = 30;
    private int killCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        killCounter = 0;

        nearbyOtherRooms = new List<GameObject>();
        swivels = new List<float>();


        player = GameObject.FindGameObjectWithTag("Player");

        facingTarge = player.transform;

        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");

        rb2D = GetComponent<Rigidbody2D>();

        waiting = 1;
        scanning = 0;

        locationCol = GetComponent<Collider2D>();


    }


    // Update is called once per frame
    void Update()
    {
        if (swivels.Count == 0)
        {
            defineSwivels();
        }

        if (health.curHealth <= 0)
        {
            Destroy(gameObject);
        }

        ////////  distance to activate
        float distance2 = Vector3.Distance(this.transform.position, player.transform.position);

        /*if (distance2 <= distance)
        {
            if (checkSightToPlayer() == true)
            {
                active = true;
            }
            else
            {
            }
        }*/
        if (active == true && sensingPlayer == false && checkSightToPlayer() == false)
        {
            active = false;

            //Debug.Log(transform.localEulerAngles.z);
            //Debug.Log(Mathf.Round(transform.localEulerAngles.z));
            //Debug.Log(transform.localEulerAngles.z -Mathf.Round(transform.localEulerAngles.z));
            float fixRotation = transform.localEulerAngles.z - Mathf.Round(transform.localEulerAngles.z);

            this.transform.Rotate(0.0f, 0.0f, -fixRotation, Space.World);

            scanning = 60;
        }
    }

    void FixedUpdate()
    {
        if (waiting > 0)
        {
            waiting--;
        }
        else
        {
            if (active == true)
            {
                setActiveBehavior();
            }
            else
            {
                if (scanForPlayer() == true)
                {
                    active = true;
                    scanning = 0;
                }
                else if (scanning > 0)
                {
                    scanning--;
                }
                else if (transform.localEulerAngles.z > (swivels[facingNumber] - .1) && transform.localEulerAngles.z < (swivels[facingNumber] + .1))
                {
                    scanning = 30;
                    if (facingNumber == swivels.Count - 1)
                    {
                        facingNumber = 0;
                    }
                    else
                    {
                        facingNumber++;
                    }
                }
                else
                {
                    this.transform.Rotate(0.0f, 0.0f, -1.0f, Space.World);
                }
            }
        }
    }

    private void setActiveBehavior()
    {
        if (isShooting == true)
        {
            if (killCounter > 0)
            {
                Vector3 vectorToTarget = facingTarge.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed / 2);

                shoot();
                waiting = 5;
                killCounter = killCounter - 5;
            }
            else
            {
                isShooting = false;
                waiting = 60;
            }
        }
        else
        {
            Vector3 vectorToTarget = facingTarge.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

            if (scanForPlayer() == true)
            {
                killCounter = killCounter + 2;
                if (killCounter >= killCounterMax)
                {
                    isShooting = true;
                }
            }
            else if (killCounter > 0)
            {
                killCounter--;
            }
        }
    }


    private void shoot()
    {

        Quaternion rotationV = new Quaternion();
        rotationV.eulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + 90);


        GameObject clone = Instantiate(bulletPrefab, this.transform.position, rotationV);
        clone.gameObject.SetActive(true);

        Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

        bullet.bulletSpeed = bulletSpeed;
        bullet.bulletFaction = (Faction)1;
        bullet.bulletDamage = bulletDamage;
    }

    private bool scanForPlayer()
    {
        if (sensingPlayer == true)
        {
            int layerMask = 1 << 0;
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, player.transform.position - this.transform.position, Mathf.Infinity, layerMask);
            if (hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        else
        {
            return false;
        }
    }

    
    private void defineSwivels()
    {
        if (locationCol.gameObject.GetComponent<storeRoomVars>().rightExit == true)
        {
            swivels.Add(0);
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().downExit == true)
        {
            swivels.Add(270);
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().leftExit == true)
        {
            swivels.Add(180);
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().upExit == true)
        {
            swivels.Add(90);
        }
    }

    private bool checkSightToPlayer()
    {
        int layerMask = 1 << 0;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, player.transform.position - this.transform.position, Mathf.Infinity, layerMask);
        if (hit.collider.gameObject.tag == "Player")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Floor")
        {
            locationCol = col;

            location = col.GetComponent<storeRoomVars>().integer;
        }
        if (col.tag == "Player") {
            sensingPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            sensingPlayer = false;
        }
    }

}

