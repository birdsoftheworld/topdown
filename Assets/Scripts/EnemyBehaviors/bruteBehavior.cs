using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bruteBehavior : MonoBehaviour
{

    private GameObject player;

    private Vector2 destination;

    public HealthTest health;

    public float distance;

    public bool active = false;

    public float speed;
    private Rigidbody2D rb2D;

    public GameObject target;

    private int waiting;

    public int location;

    public Collider2D locationCol;
    private GameObject levelGen;

    private Vector2 nodeDirectionModifier;

    public Vector2 adjustTarge = new Vector2(0, 0);
    public Vector2 coverTarge = new Vector2(0, 0);

    public bool isCharging = false;

    public Vector2 chargeTarget = new Vector2(0, 0);

    public int firingSpeed;
    public int ammoCap;
    public int ammo;
    public int bulletSpeed;
    public int bulletDamage;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ammo = ammoCap;

        player = GameObject.FindGameObjectWithTag("Player");

        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");

        rb2D = GetComponent<Rigidbody2D>();

        target = player;

        waiting = 0;

        locationCol = GetComponent<Collider2D>();

        nodeDirectionModifier = new Vector2(0.0f, 0.0f);

        adjustTarge = findNearestNodeOfType("NodeEnter").position;
        coverTarge = findNearestNodeOfType("NodeCover").position;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.curHealth <= 0)
        {
            Destroy(gameObject);
        }

        ////////  distance to activate
        float distance2 = Vector3.Distance(this.transform.position, player.transform.position);

        if (distance2 <= distance)
        {
            if (checkSightToPlayer() == true)
            {
                active = true;
                onActivate();
            }
            else
            {
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
        //rb2D.velocity = rb2D.velocity / 3;
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
            }
        }
    }

    private void setActiveBehavior()
    {

        if (isCharging)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, chargeTarget, speed * 2 * Time.deltaTime);


            //rb2D.AddForce(transform.up * speed * -200f);
            if (Vector3.Distance(this.transform.position, chargeTarget) < .1)
            {
                isCharging = false;
                waiting = 15;
                Debug.Log("miss!");
            }
            else if (Vector3.Distance(this.transform.position, player.transform.position) < 1.1)
            {
                isCharging = false;
                waiting = 10;
                Debug.Log("slap!");
            }
        }
        else if (location == player.GetComponent<Player>().location)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) > 1 && Vector3.Distance(this.transform.position, player.transform.position) < 6) // charge!
            {
                FaceTarget(player.transform.position);
                //Debug.Log(this.transform.rotation.eulerAngles.z);
                float x1 = this.transform.position.x;
                float y1 = this.transform.position.y;
                float x2 = 5 * Mathf.Sin(this.transform.rotation.eulerAngles.z);
                float y2 = 5 * Mathf.Cos(this.transform.rotation.eulerAngles.z);

                chargeTarget = new Vector2(x1 + x2, y1+ y2);
                isCharging = true;

            }
            else
            {

                this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * 2 * Time.deltaTime);

                
                //Vector3.Distance(this.transform.position, closestE.position)
            }
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

    private void FaceTarget(Vector2 targetPos)
    {
        Vector2 currentPos = this.transform.position;
        destination = targetPos - currentPos;
        float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = rotation;
    }

    private void onActivate()
    {

    }

    private void onDeactivate()
    {
    }

    private void shoot()
    {
        ammo--;
        FaceTarget(player.transform.position);

        GameObject clone = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        clone.gameObject.SetActive(true);

        Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

        bullet.bulletSpeed = bulletSpeed;
        bullet.bulletFaction = (Faction)1;
        bullet.bulletDamage = bulletDamage;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Floor")
        {
            locationCol = col;

            location = col.GetComponent<storeRoomVars>().integer;
        }
    }

    private Transform closestExit()
    {
        Transform closest;
        float distance;
        closest = this.transform;
        distance = 9999;

        if (locationCol.gameObject.GetComponent<storeRoomVars>().upExit == true)
        {
            closest = levelGen.GetComponent<Level>().roomData[location][1];

            distance = Vector3.Distance(this.transform.position, closest.transform.position);

            nodeDirectionModifier = new Vector2(0.0f, 5.0f);
            //Debug.Log("up");
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().downExit == true)
        {
            if (closest == this.transform)
            {
                closest = levelGen.GetComponent<Level>().roomData[location][11];

                nodeDirectionModifier = new Vector2(0.0f, -5.0f);
                //Debug.Log("down");
            }
            else
            {
                float distance2 = Vector3.Distance(this.transform.position, levelGen.GetComponent<Level>().roomData[location][11].transform.position);
                if (distance2 < distance)
                {
                    closest = levelGen.GetComponent<Level>().roomData[location][11];

                    nodeDirectionModifier = new Vector2(0.0f, -5.0f);
                    //Debug.Log("down");

                }
            }
            distance = Vector3.Distance(this.transform.position, closest.transform.position);
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().leftExit == true)
        {
            if (closest == this.transform)
            {
                closest = levelGen.GetComponent<Level>().roomData[location][5];

                nodeDirectionModifier = new Vector2(-5.0f, 0.0f);
                //Debug.Log("left");

            }
            else
            {
                float distance2 = Vector3.Distance(this.transform.position, levelGen.GetComponent<Level>().roomData[location][5].transform.position);

                if (distance2 < distance)
                {
                    closest = levelGen.GetComponent<Level>().roomData[location][5];

                    nodeDirectionModifier = new Vector2(-5.0f, 0.0f);
                    //Debug.Log("left");

                }
            }
            distance = Vector3.Distance(this.transform.position, closest.transform.position);
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().rightExit == true)
        {
            if (closest == this.transform)
            {
                closest = levelGen.GetComponent<Level>().roomData[location][7];

                nodeDirectionModifier = new Vector2(5.0f, 0.0f);
                //Debug.Log("right");

            }
            else
            {
                float distance2 = Vector3.Distance(this.transform.position, levelGen.GetComponent<Level>().roomData[location][7].transform.position);

                if (distance2 < distance)
                {
                    closest = levelGen.GetComponent<Level>().roomData[location][7];

                    nodeDirectionModifier = new Vector2(5.0f, 0.0f);
                    //Debug.Log("right");

                }
            }
            distance = Vector3.Distance(this.transform.position, closest.transform.position);
        }
        return closest;
    }

    private Transform findCover() // finds closest cover that's furthest from player
    {
        int lastNodeChosen = 7;
        int lastNodeChosen2 = 7;

        if (locationCol.gameObject.GetComponent<storeRoomVars>().upExit == true)
        {
            int nodeChoice = checkTwoNodeDistances(0, 2, this.transform);
            lastNodeChosen = nodeChoice;

            if (lastNodeChosen == 0)
            {
                lastNodeChosen2 = 2;
            }
            else
            {
                lastNodeChosen2 = 0;
            }
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().downExit == true)
        {
            if (lastNodeChosen == 7)
            {
                int nodeChoice = checkTwoNodeDistances(10, 12, this.transform);
                lastNodeChosen = nodeChoice;

                if (lastNodeChosen == 10)
                {
                    lastNodeChosen2 = 12;
                }
                else
                {
                    lastNodeChosen2 = 10;
                }
            }
            else
            {
                int nodeChoice = checkTwoNodeDistances(10, 12, this.transform);
                if (checkTwoNodeDistances(nodeChoice, lastNodeChosen, this.transform) == nodeChoice)
                {
                    lastNodeChosen = nodeChoice;

                    if (lastNodeChosen == 10)
                    {
                        lastNodeChosen2 = 12;
                    }
                    else
                    {
                        lastNodeChosen2 = 10;
                    }
                }
            }
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().leftExit == true)
        {
            if (lastNodeChosen == 7)
            {
                int nodeChoice = checkTwoNodeDistances(3, 8, this.transform);
                lastNodeChosen = nodeChoice;

                if (lastNodeChosen == 3)
                {
                    lastNodeChosen2 = 8;
                }
                else
                {
                    lastNodeChosen2 = 3;
                }
            }
            else
            {
                int nodeChoice = checkTwoNodeDistances(3, 8, this.transform);
                if (checkTwoNodeDistances(nodeChoice, lastNodeChosen, this.transform) == nodeChoice)
                {
                    lastNodeChosen = nodeChoice;

                    if (lastNodeChosen == 3)
                    {
                        lastNodeChosen2 = 8;
                    }
                    else
                    {
                        lastNodeChosen2 = 3;
                    }
                }
            }
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().rightExit == true)
        {
            if (lastNodeChosen == 7)
            {
                int nodeChoice = checkTwoNodeDistances(4, 9, this.transform);
                lastNodeChosen = nodeChoice;

                if (lastNodeChosen == 4)
                {
                    lastNodeChosen2 = 9;
                }
                else
                {
                    lastNodeChosen2 = 4;
                }
            }
            else
            {
                int nodeChoice = checkTwoNodeDistances(4, 9, this.transform);
                if (checkTwoNodeDistances(nodeChoice, lastNodeChosen, this.transform) == nodeChoice)
                {
                    lastNodeChosen = nodeChoice;

                    if (lastNodeChosen == 4)
                    {
                        lastNodeChosen2 = 9;
                    }
                    else
                    {
                        lastNodeChosen2 = 4;
                    }
                }
            }
        }

        if (checkTwoNodeDistances(lastNodeChosen, lastNodeChosen2, player.transform) == lastNodeChosen)
        {
            return levelGen.GetComponent<Level>().roomData[location][lastNodeChosen2];
        }
        else
        {
            return levelGen.GetComponent<Level>().roomData[location][lastNodeChosen];
        }
    }

    public Transform findNearestNodeOfType(string nodeTag)
    {
        //find all cover
        GameObject[] cover;

        cover = GameObject.FindGameObjectsWithTag(nodeTag);
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
        return closest.transform;
    }

    public int checkTwoNodeDistances(int node1, int node2, Transform target)
    {
        float distance2 = Vector3.Distance(target.position, levelGen.GetComponent<Level>().roomData[location][node1].transform.position);
        float distance3 = Vector3.Distance(target.transform.position, levelGen.GetComponent<Level>().roomData[location][node2].transform.position);

        if (distance2 < distance3)
        {
            return node1;
        }
        else
        {
            return node2;
        }
    }

    /*public bool isPlayerNearby()
    {
        
    }*/
}