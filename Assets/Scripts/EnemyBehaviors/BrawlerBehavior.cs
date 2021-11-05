using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawlerBehavior : MonoBehaviour
{

    private GameObject player;

    private Vector2 destination;

    public HealthTest health;

    public float distance;

    public bool active = false;

    public float speed;
    private Rigidbody2D rb2D;

    public GameObject target;

    public int waiting;

    public int location;

    public Collider2D locationCol;
    private GameObject levelGen;

    private Vector2 nodeDirectionModifier;

    public bool movingToExit = false;
    public bool movingToCenter = false;
    public bool isShooting = false;

    public Vector2 adjustTarge = new Vector2(0, 0);
    public Vector2 coverTarge = new Vector2(0, 0);
    public Vector2 centerTarge = new Vector2(0, 0);


    public int bulletSpeed;
    public int bulletDamage;
    public GameObject bulletPrefab;
    public GameObject tracerPrefab;

    private Transform chasingTarget;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");

        rb2D = GetComponent<Rigidbody2D>();

        target = player;

        waiting = 0;

        locationCol = GetComponent<Collider2D>();

        nodeDirectionModifier = new Vector2(0.0f, 0.0f);

        chasingTarget = player.transform;

        //adjustTarge = findNearestNodeOfType("NodeEnter", this.transform).position;
        //coverTarge = findNearestNodeOfType("NodeCover", this.transform).position;

    }

    // Update is called once per frame
    void Update()
    {
        if (health.curHealth <= 0)
        {
            levelGen.GetComponent<LootController>().Drop(this.transform.position, 2, 1, 5);
            Destroy(gameObject);
        }

        //Debug.Log("player two away?" + playerTwoRoomsAway());

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
                if (findNearestNodeOfType("Projectile", this.transform).position != this.transform.position)
                {
                    if (Vector2.Distance(findNearestNodeOfType("Projectile", this.transform).position, this.transform.position) < 2)
                    {
                        active = true;
                    }
                }
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
        //Debug.Log(playerNear());

        if (checkSightToPlayer() == true)
        {
            movingToCenter = false;
            movingToExit = false;
        }

        bool nearEnough = false;
        if (Vector2.Distance(this.transform.position, player.transform.position) < 22)
        {
            nearEnough = true;
        }
        Debug.Log(nearEnough);

        if (isShooting == true)
        {
            if (Vector2.Distance(this.transform.position, player.transform.position) > 1)
            {
                isShooting = false;
            }
        }
        else if (Vector2.Distance(this.transform.position, player.transform.position) < 1)
        {
            isShooting = true;
        }
        else if (checkSightToPlayer() == true)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * 2 * Time.deltaTime);
        }
        else if (location == player.GetComponent<Player>().location)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * 2 * Time.deltaTime);
        }
        else if (movingToExit == true)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, chasingTarget.position, speed * 2 * Time.deltaTime);

            if (Vector2.Distance(this.transform.position, chasingTarget.position) < 1)
            {
                movingToExit = false;
            }
        }
        else if (playerNear() == true || nearEnough == true)
        {
            if (Vector2.Distance(findNearestNodeOfType("NodeEnter", this.transform).position, this.transform.position) < 1)
            {
                movingToCenter = false;
                movingToExit = true;

                if (playerNear() == true)
                {
                    chasingTarget = findNearestNodeOfType("NodeEnter", playerNearRoomTransform());
                }
                else
                {
                    chasingTarget = findNearestNodeOfType("NodeCenter", player.transform);
                }
            }
            else if (movingToCenter == true)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, findNearestNodeOfType("NodeEnter", this.transform).position, speed * 2 * Time.deltaTime);
            }
            else
            {
                movingToCenter = true;
            }
        }
        else
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, findNearestNodeOfType("NodeCenter", this.transform).position, speed * 2 * Time.deltaTime);
        }
    }
    /*if (Vector3.Distance(this.transform.position, player.transform.position) < 1)
    {
        inPanic = true;
    }*/
    //}

    private Transform playerNearRoomTransform() //ADD TO OTHER ENEMY!!!!!!!?????????????????? MAYBE CAN ONLY DETECT ORTHANGONAL ADJACENCY IDK
    {
        List<GameObject> playerNears = new List<GameObject>();
        List<GameObject> meNears = new List<GameObject>();

        GameObject[] centralNodes;

        centralNodes = GameObject.FindGameObjectsWithTag("NodeCenter");
        Vector3 position = this.transform.position;

        Transform playerCenter = findNearestNodeOfType("NodeCenter", player.transform);
        Transform meCenter = findNearestNodeOfType("NodeCenter", this.transform);


        foreach (GameObject go in centralNodes)
        {
            //Debug.Log(Vector2.Distance(playerCenter.position, go.transform.position));
            if (Vector2.Distance(playerCenter.position, go.transform.position) == 11)
            {
                playerNears.Add(go);
                //Debug.Log(go.transform.position);
            }
        }
        foreach (GameObject go2 in centralNodes)
        {
            //Debug.Log(Vector2.Distance(meCenter.position, go2.transform.position));
            if (Vector2.Distance(meCenter.position, go2.transform.position) == 11)
            {
                meNears.Add(go2);
                //Debug.Log(go2.transform.position);
            }
        }
        //Debug.Log(playerNears.Count);
        //Debug.Log("----");
        for (int i = 0; i < playerNears.Count; i++)
        {
            //Debug.Log("p " + playerNears[i].transform.position);
            for (int b = 0; b < meNears.Count; b++)
            {
                //Debug.Log("i " + meNears[b].transform.position);

                if (playerNears[i].transform.position == meNears[b].transform.position)
                {
                    //Debug.Log("success!!");
                    return playerNears[i].transform;
                }
            }
        }

        return null;
    }

    private bool playerNear() //ADD TO OTHER ENEMY!!!!!!!?????????????????? MAYBE CAN ONLY DETECT ORTHANGONAL ADJACENCY IDK
    {
        List<GameObject> playerNears = new List<GameObject>();
        List<GameObject> meNears = new List<GameObject>();

        GameObject[] centralNodes;

        centralNodes = GameObject.FindGameObjectsWithTag("NodeCenter");
        Vector3 position = this.transform.position;

        Transform playerCenter = findNearestNodeOfType("NodeCenter", player.transform);
        Transform meCenter = findNearestNodeOfType("NodeCenter", this.transform);


        foreach (GameObject go in centralNodes)
        {
            //Debug.Log(Vector2.Distance(playerCenter.position, go.transform.position));
            if (Vector2.Distance(playerCenter.position, go.transform.position) == 11)
            {
                playerNears.Add(go);
                //Debug.Log(go.transform.position);
            }
        }
        foreach (GameObject go2 in centralNodes)
        {
            //Debug.Log(Vector2.Distance(meCenter.position, go2.transform.position));
            if (Vector2.Distance(meCenter.position, go2.transform.position) == 11)
            {
                meNears.Add(go2);
                //Debug.Log(go2.transform.position);
            }
        }
        //Debug.Log(playerNears.Count);
        //Debug.Log("----");
        for (int i = 0; i < playerNears.Count; i++)
        {
            //Debug.Log("p " + playerNears[i].transform.position);
            for (int b = 0; b < meNears.Count; b++)
            {
                //Debug.Log("i " + meNears[b].transform.position);

                if (playerNears[i].transform.position == meNears[b].transform.position)
                {
                    //Debug.Log("success!!");
                    return true;
                }
            }
        }

        return false;
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
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<Level>().roomData[player.GetComponent<Player>().location][6].position, this.transform.position) > 13)
        {
            StartCoroutine("FindCoverCoroutine");
            return findNearestNodeOfType("NodeCenter", player.transform);
            //movingToNode = true;
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

    IEnumerator FindCoverCoroutine()
    {
        yield return new WaitForSeconds(5 / 4);
        coverTarge = findCover().position;
    }


    public Transform findNearestNodeOfType(string nodeTag, Transform from)
    {
        //find all cover
        GameObject[] cover;

        cover = GameObject.FindGameObjectsWithTag(nodeTag);
        GameObject closest = this.gameObject;
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

        int ticker = 0;

        for (int i = 0; i < (gatherer.Length - 1); i++)
        {
            //Debug.Log(exits[i].transform.position);

            rooms.Add(gatherer[i].transform);
            //exitDistances.Add(Vector3.Distance(this.transform.position, exits[i].transform.position));
        }
        for (int i = 0; i < (gatherer.Length - 1); i++)
        {

            if (searchFrom.GetComponent<Player>() == null)
            {
                if (locationCol.gameObject.transform == rooms[i])
                {
                    //Debug.Log("this room");
                    //nothing should happen; this is the room they're in already
                }
                else if (Vector3.Distance(locationCol.gameObject.transform.position, rooms[i].position) == 11 || Vector3.Distance(locationCol.gameObject.transform.position, rooms[i].position) == -11/* && Vector3.Distance(locationCol.gameObject.transform.position, rooms[i].position) < 22*/)
                {
                    //Debug.Log(locationCol.gameObject.transform.position);
                    //Debug.Log("me " + rooms[i].position);
                    finalRooms[ticker] = rooms[i];
                    ticker++;
                    //Debug.Log(rooms[i].position);
                }
            }
            else
            {
                Transform playerRoom = this.transform;

                for (int b = 0; b < (gatherer.Length - 1); b++)
                {
                    //Debug.Log("leg " + gatherer.Length);
                    //Debug.Log("gat " + gatherer[b].GetComponent<storeRoomVars>().integer);
                    //Debug.Log("pla " + searchFrom.GetComponent<Player>().location);

                    if (gatherer[b].GetComponent<storeRoomVars>().integer == searchFrom.GetComponent<Player>().location)
                    {
                        playerRoom = gatherer[b].transform;

                        //Debug.Log(playerRoom.position);
                    }
                }
                //Debug.Log(playerRoom.transform.position);
                //Debug.Log(rooms[i].position);

                //Debug.Log(Vector3.Distance(playerRoom.transform.position, rooms[i].position));

                /*if (playerRoom == rooms[i])
                {
                    //nothing should happen; this is the room they're in already
                }
                else */
                if (Vector3.Distance(playerRoom.transform.position, rooms[i].position) == 11 || Vector3.Distance(playerRoom.transform.position, rooms[i].position) == -11/*&& Vector3.Distance(locationCol.gameObject.transform.position, rooms[i].position) < 22*/)
                {
                    //Debug.Log(playerRoom.transform.position);
                    //Debug.Log("player " + rooms[i].position);
                    finalRooms[ticker] = rooms[i];
                    //Debug.Log(rooms[i].position);
                    //Debug.Log(finalRooms[ticker].position);
                    ticker = ticker + 1;
                }
            }
        }
        for (int i = 0; i < 4; i++)

        {


            if (finalRooms[i] == null)
            {
                finalRooms[i] = this.transform;
            }
            //Debug.Log(finalRooms[i].position);

            if (searchFrom.GetComponent<Player>() != null)
            {
                //Debug.Log(finalRooms[i].position);
            }

        }
        return (finalRooms);
    }

    public bool playerTwoRoomsAway()
    {
        Transform[] nearToMe = nearbyRoom(this.transform.gameObject);
        Transform[] nearToPlayer = nearbyRoom(player);
        //Transform intersection;

        //Debug.Log(nearToPlayer.Length);
        //Debug.Log("player !!!!" + nearToPlayer[1].position);


        for (int i = 0; i < 4; i++)
        {
            //Debug.Log("---------------");
            for (int a = 0; a < 4; a++)
            {
                //Debug.Log("me " + nearToMe[i].position);
                //Debug.Log("player " + nearToPlayer[a].position);


                if (nearToMe[i] == nearToPlayer[a] && nearToMe[i] != this.transform)
                {

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
        GameObject intersection;

        //Debug.Log(nearToPlayer.Length);
        //Debug.Log("player !!!!" + nearToPlayer[1].position);


        for (int i = 0; i < 4; i++)
        {
            //Debug.Log("---------------");
            for (int a = 0; a < 4; a++)
            {
                //Debug.Log("me " + nearToMe[i].position);
                //Debug.Log("player " + nearToPlayer[a].position);


                if (nearToMe[i] == nearToPlayer[a] && nearToMe[i] != this.transform)
                {

                    intersection = nearToMe[i].gameObject;
                    return intersection;
                }
            }
        }
        return null;
    }
}