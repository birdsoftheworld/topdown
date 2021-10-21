using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistoleerBehavior : MonoBehaviour
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

    public bool movingToNode = false;
    public bool inCover = false;
    public bool isShooting = false;
    public bool inPanic = false;

    public Vector2 adjustTarge = new Vector2(0, 0);
    public Vector2 coverTarge = new Vector2(0, 0);

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

        adjustTarge = findNearestNodeOfType("NodeEnter", this.transform).position;
        coverTarge = findNearestNodeOfType("NodeCover", this.transform).position;
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
        if (isShooting == true)
        {
            if (ammo > 0)
            {
                if (checkSightToPlayer() == true)
                {
                    shoot();
                    if (checkSightToPlayer() == true)
                    {
                        waiting = firingSpeed;
                    }
                    else
                    {
                        isShooting = false;
                    }
                }
                else
                {
                    isShooting = false;
                }
            }
            else
            {
                isShooting = false;
            }
        }
        /*else if (inPanic == true)
        {
            FaceTarget(player.transform.position);
            transform.Rotate(0, 0, 180);
            rb2D.AddForce(transform.up * speed * -100f);
            if (Vector3.Distance(this.transform.position, player.transform.position) > 6)
            {
                inPanic = false;
            }
        }*/
        else if (location == player.GetComponent<Player>().location || movingToNode == true)
        {
            inCover = false;
            Transform closestE = closestExit(0);

            if (Vector3.Distance(this.transform.position, closestE.position) > 1 && movingToNode == false)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, closestE.position, speed * 2 * Time.deltaTime);

                float a1 = closestE.position.x;
                float b1 = closestE.position.y;
                float a2 = nodeDirectionModifier.x;
                float b2 = nodeDirectionModifier.y;

                adjustTarge = new Vector2(a1 + a2, b1 + b2);
                movingToNode = true;
            }

            else
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, adjustTarge, speed * 2 * Time.deltaTime);

                if (Vector2.Distance(adjustTarge, this.transform.position) < 1)
                {
                    movingToNode = false;
                    coverTarge = findCover().position;
                }
            }
        }
        else
        {
            if (playerTwoRoomsAway() == true)
            {
                Debug.Log("player 2 away");
                GameObject roomGoTo = sharedNearRoom();

                //Vector2 targetNode = 

            }
            else if (inCover == false)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, coverTarge, speed * 2 * Time.deltaTime);

                if (Vector2.Distance(coverTarge, this.transform.position) < .1)
                {
                    inCover = true;
                }
            }
            else
            {
                if (ammo > 0)
                {
                    int layerMask = 1 << 0;
                    RaycastHit2D hit;
                    hit = Physics2D.Raycast(findNearestNodeOfType("NodeEnter", this.transform).position, player.transform.position - findNearestNodeOfType("NodeEnter", this.transform).position, Mathf.Infinity, layerMask);
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        this.transform.position = Vector2.MoveTowards(this.transform.position, findNearestNodeOfType("NodeEnter", this.transform).position, speed * 2 * Time.deltaTime);

                        if (Vector2.Distance(findNearestNodeOfType("NodeEnter", this.transform).position, this.transform.position) < .1)
                        {
                            isShooting = true;
                            inCover = false;
                            coverTarge = findCover().position;
                        }
                    }
                    else
                    {
                        inCover = false;
                        coverTarge = findCover().position;
                        if (ammo < ammoCap)
                        {
                            waiting = waiting + 7;
                            ammo++;
                        }
                    }
                }
                else
                {
                    waiting = waiting + (7 * ammoCap);
                    ammo = ammoCap;
                }
            }
        }
        /*if (Vector3.Distance(this.transform.position, player.transform.position) < 1)
        {
            inPanic = true;
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

    private Transform closestExit(int positionFind)
    {
        GameObject[] exits;

        exits = GameObject.FindGameObjectsWithTag("NodeEnter");

        List<Transform> trueExits = new List<Transform>();
        List<float> exitDistances = new List<float>();

        //Debug.Log(exits.Length);

        for (int i = 0; i < (exits.Length - 1); i++)
        {
            //Debug.Log(exits[i].transform.position);

            trueExits.Add(exits[i].transform);
            exitDistances.Add(Vector3.Distance(this.transform.position, exits[i].transform.position));
        }

        exitDistances.Sort();

        for (int i = 0; i < exits.Length - 1; i++)
        {
            if (Vector3.Distance(this.transform.position, trueExits[i].position) == exitDistances[positionFind])
            {
                //Debug.Log(trueExits[i].position);

                if (trueExits[i].position.y > findNearestNodeOfType("NodeCenter", this.transform).position.y)
                {
                    nodeDirectionModifier = new Vector2(0.0f, 4.0f);
                }
                if (trueExits[i].position.y < findNearestNodeOfType("NodeCenter", this.transform).position.y)
                {
                    nodeDirectionModifier = new Vector2(0.0f, -4.0f);
                }
                if (trueExits[i].position.x > findNearestNodeOfType("NodeCenter", this.transform).position.x)
                {
                    nodeDirectionModifier = new Vector2(4.0f, 0.0f);
                }
                if (trueExits[i].position.x < findNearestNodeOfType("NodeCenter", this.transform).position.x)
                {
                    nodeDirectionModifier = new Vector2(-4.0f, 0.0f);
                }

                return trueExits[i];
            }
        }

        Debug.Log("total failure");
        return this.transform;

        //return exitDistances[positionFind];

        //trueExits.Sort(exitDistances);
    }


    private Transform closestExit2(int useless)
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

    public Transform findNearestNodeOfType(string nodeTag, Transform from)
    {
        //find all cover
        GameObject[] cover;

        cover = GameObject.FindGameObjectsWithTag(nodeTag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = from.position;
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

    public Transform[] nearbyRoom(GameObject searchFrom)
    {
        GameObject[] gatherer;

        gatherer = GameObject.FindGameObjectsWithTag("Floor");

        List<Transform> rooms = new List<Transform>();

        Transform[] finalRooms = new Transform[4];

        for (int i = 0; i < (gatherer.Length - 1); i++)
        {
            //Debug.Log(exits[i].transform.position);

            rooms.Add(gatherer[i].transform);
            //exitDistances.Add(Vector3.Distance(this.transform.position, exits[i].transform.position));
        }
        for (int i = 0; i < (gatherer.Length - 1); i++)
        {
            int ticker = 0;

            if (searchFrom.GetComponent<Player>() == null)
            {
                if (locationCol.gameObject.transform == rooms[i])
                {
                    //nothing should happen; this is the room they're in already
                }
                else if (Vector3.Distance(locationCol.gameObject.transform.position, rooms[i].position) == 11)
                {
                    finalRooms[ticker] = rooms[i];
                    ticker++;
                }
            }
            else
            {
                Transform playerRoom = this.transform;

                for (int b = 0; b < (gatherer.Length - 1); b++)
                {
                    if (gatherer[b].GetComponent<storeRoomVars>().integer == searchFrom.GetComponent<Player>().location)
                    {
                        playerRoom = gatherer[b].transform;
                    }
                }

                if (playerRoom == rooms[i])
                {
                    //nothing should happen; this is the room they're in already
                }
                else if (Vector3.Distance(locationCol.gameObject.transform.position, rooms[i].position) == 11)
                {
                    finalRooms[ticker] = rooms[i];
                    ticker++;
                }
            }
        }
        for (int i = 0; i < 4; i++)

        {
            if (finalRooms[i] == null)
            {
                finalRooms[i] = this.transform;
            }
        }
        return (finalRooms);
    }

    public bool playerTwoRoomsAway()
    {
        Transform[] nearToMe = nearbyRoom(this.transform.gameObject);
        Transform[] nearToPlayer = nearbyRoom(player);
        //Transform intersection;

        for (int i = 0; i < 4; i++)
        {
            for (int a = 0; a < 4; a++)
            {
                if (nearToMe[i].position == nearToPlayer[a].position)
                {
                    Debug.Log(nearToMe[i].position);//WRONG WRONG WRONG WRONG
                    //intersection = nearToMe[i];
                    return true;
                }
            }
        }
        return false;
    }

    public GameObject sharedNearRoom()
    {
        Transform[] nearToMe = nearbyRoom(this.transform.gameObject);
        Transform[] nearToPlayer = nearbyRoom(player);
        Transform intersection = this.transform;

        for (int i = 0; i < 4; i++)
        {
            for (int a = 0; a < 4; a++)
            {
                if (nearToMe[i].position == nearToPlayer[a].position)
                {
                    intersection = nearToMe[i];
                }
            }
        }

        return intersection.gameObject;
    }

}