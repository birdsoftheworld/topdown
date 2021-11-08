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

    public GameObject locationCol;
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

    private float swingnumber = -1;

    public TurretCone TC;

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

        waiting = 5;
        scanning = 0;

        locationCol = this.gameObject;// GetComponent<Collider2D>();

        TC.gameObject.SetActive(false);

        //health = transform.GetChild(0).GetComponent<HealthTest>();

    }


    // Update is called once per frame
    void Update()
    {

        sensingPlayer = TC.sensingPlayer;

        if (health.curHealth <= 0)
        {
            levelGen.GetComponent<LootController>().Drop(this.transform.position, 0, 1, 2);
            levelGen.GetComponent<LootController>().Drop(this.transform.position, 1, 5, 2);
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
            if (swivels.Count == 0)
            {
                defineSwivels();
                TC.gameObject.SetActive(true);
            }
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

                    float a = Mathf.Round(transform.localEulerAngles.z);
                    //Debug.Log(a);
                    if (a == 0 || a == 1)
                    {
                        if (swivels[facingNumber] == 90)
                        {
                            swingnumber = 1f;
                        }
                        else
                        {
                            swingnumber = -1f;
                        }
                    }
                    else if (transform.localEulerAngles.z > swivels[facingNumber])
                    {
                        swingnumber = -1f;
                    }
                    else
                    {
                        swingnumber = 1f;
                    }
                }
                else
                {
                    this.transform.Rotate(0.0f, 0.0f, swingnumber, Space.World);
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
            this.transform.GetChild(0).gameObject.SetActive(false);
            int layerMask = 1 << 0;
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, player.transform.position - this.transform.position, Mathf.Infinity, layerMask);
            if (hit.collider.gameObject.tag == "Player")
            {
                this.transform.GetChild(0).gameObject.SetActive(true);
                return true;
            }
            else
            {
                //Debug.Log(hit.collider.gameObject.tag);
                this.transform.GetChild(0).gameObject.SetActive(true);
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
            for (int i = 0; i < swivels.Count; i++)
            {
                swivels.Remove(i);
            }
        //Debug.Log(GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<Level>().roomData[location][6].transform.parent.gameObject.transform.position);
        //Debug.Log(GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<Level>().roomData[location][6].transform.parent.gameObject.transform.parent.gameObject.GetComponent<storeRoomVars>().rightExit);
        //if (GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<Level>().roomData[location][6].transform.parent.gameObject.transform.parent.gameObject.GetComponent<storeRoomVars>() != null)
        //{
        if (locationCol.GetComponent<storeRoomVars>() != null) 
        {
            storeRoomVars roomVars = locationCol.GetComponent<storeRoomVars>();//GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<Level>().roomData[location][6].transform.parent.gameObject.transform.parent.gameObject.GetComponent<storeRoomVars>();
            //Debug.Log(checker1.transform.position);
            //Debug.Log(roomVars.rightExit);
            if (roomVars.rightExit == true)
            {
                swivels.Add(0);
            }
            if (roomVars.downExit == true)
            {
                swivels.Add(270);
            }
            if (roomVars.leftExit == true)
            {
                swivels.Add(180);
            }
            if (roomVars.upExit == true)
            {
                swivels.Add(90);
            }
        }

        //Debug.Log(checker2.transform.position);
        /*if (checker2.gameObject.GetComponent<storeRoomVars>().rightExit == true)
        {
            swivels.Add(0);
        }
        if (checker2.gameObject.GetComponent<storeRoomVars>().downExit == true)
        {
            swivels.Add(270);
        }
        if (checker2.gameObject.GetComponent<storeRoomVars>().leftExit == true)
        {
            swivels.Add(180);
        }
        if (checker2.gameObject.GetComponent<storeRoomVars>().upExit == true)
        {
            swivels.Add(90);
        }*/
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


    void OnCollisionEnter2D(Collision2D col)
    {
        if (TC.gameObject.activeSelf == false)
        {
            if (col.gameObject.tag == "Floor")
            {
                locationCol = col.gameObject;

                location = col.gameObject.GetComponent<storeRoomVars>().integer;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (TC.gameObject.activeSelf == false)
        {
            if (col.tag == "Floor")
            {
                locationCol = col.gameObject;

                location = col.GetComponent<storeRoomVars>().integer;
            }
        }
    }
}