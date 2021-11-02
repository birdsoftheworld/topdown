using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeNavigation : MonoBehaviour
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

    public int location;

    public Collider2D locationCol;
    private GameObject levelGen;

    private Vector2 nodeDirectionModifier;

    public bool movingToNode = false;
    public bool inCover = false;
    public bool isShooting = false;

    public Vector2 adjustTarge = new Vector2(0,0);

    public Vector2 coverTarge = new Vector2(0, 0);

    public int firingSpeed = 10;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");

        rb2D = GetComponent<Rigidbody2D>();

        target = player;

        targetL = target.transform;

        waiting = 0;

        locationCol = GetComponent<Collider2D>();

        nodeDirectionModifier = new Vector2(0.0f, 0.0f);

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
        rb2D.velocity = rb2D.velocity / 3;
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

                /*this.transform.position = Vector2.MoveTowards(this.transform.position, findNearestNodeOfType("NodeCenter").position, speed * 2 * Time.deltaTime);
                if (Vector2.Distance(findNearestNodeOfType("NodeCenter").position, this.transform.position) < .1)
                {
                    isShooting = false;
                    inCover = false;
                    coverTarge = findCover().position;
                }*/
            }
        }
        else if (location == player.GetComponent<Player>().location || movingToNode == true)
        {
            inCover = false;
            Transform closestE = closestExit();
            
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
            if (inCover == false)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, coverTarge, speed * 2 * Time.deltaTime);

                if (Vector2.Distance(coverTarge, this.transform.position) < .1)
                {
                    inCover = true;
                }
            }
            else
            {
                int layerMask = 1 << 0;
                RaycastHit2D hit;
                hit = Physics2D.Raycast(findNearestNodeOfType("NodeEnter").position, player.transform.position - findNearestNodeOfType("NodeEnter").position, Mathf.Infinity, layerMask);
                if (hit.collider.gameObject.tag == "Player")
                {
                    this.transform.position = Vector2.MoveTowards(this.transform.position, findNearestNodeOfType("NodeEnter").position, speed * 2 * Time.deltaTime);

                    if (Vector2.Distance(findNearestNodeOfType("NodeEnter").position, this.transform.position) < .1)
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

                    /*this.transform.position = Vector2.MoveTowards(this.transform.position, findNearestNodeOfType("NodeCenter").position, speed * 2 * Time.deltaTime);
                    if (Vector2.Distance(findNearestNodeOfType("NodeCenter").position, this.transform.position) < .1)
                    {
                        isShooting = true;
                        inCover = false;
                        coverTarge = findCover().position;
                    }*/

                }

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
        Debug.Log("bang!");
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

    /*
        private Transform findCover3(int valueGet)
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

            if (valueGet = 1)
            {
                return levelGen.GetComponent<Level>().roomData[location][lastNodeChosen2];
            }
            else
            {
                return levelGen.GetComponent<Level>().roomData[location][lastNodeChosen];
            }
        }

        private Transform findCover2()
        {
            int lastNodeChosen = 7;

            if (locationCol.gameObject.GetComponent<storeRoomVars>().upExit == true)
            {
                int nodeChoice = checkTwoNodeDistances(0, 2, this.transform);

                lastNodeChosen = nodeChoice;

                //nodeDirectionModifier = new Vector2(0.0f, 5.0f);
                //Debug.Log("up");
            }
            if (locationCol.gameObject.GetComponent<storeRoomVars>().downExit == true)
            {
                if (lastNodeChosen == 7)
                {
                    int nodeChoice = checkTwoNodeDistances(10, 12, this.transform);

                    lastNodeChosen = nodeChoice;

                }
                else
                {
                    int nodeChoice = checkTwoNodeDistances(10, 12, this.transform);

                    if (checkTwoNodeDistances(nodeChoice, lastNodeChosen, this.transform) == nodeChoice)
                    {
                        lastNodeChosen = nodeChoice;
                    }
                }
            }
            if (locationCol.gameObject.GetComponent<storeRoomVars>().leftExit == true)
            {
                if (lastNodeChosen == 7)
                {
                    int nodeChoice = checkTwoNodeDistances(3, 8, this.transform);

                    lastNodeChosen = nodeChoice;

                }
                else
                {
                    int nodeChoice = checkTwoNodeDistances(3, 8, this.transform);

                    if (checkTwoNodeDistances(nodeChoice, lastNodeChosen, this.transform) == nodeChoice)
                    {
                        lastNodeChosen = nodeChoice;
                    }
                }
            }
            if (locationCol.gameObject.GetComponent<storeRoomVars>().rightExit == true)
            {
                if (lastNodeChosen == 7)
                {
                    int nodeChoice = checkTwoNodeDistances(4, 9, this.transform);

                    lastNodeChosen = nodeChoice;

                }
                else
                {
                    int nodeChoice = checkTwoNodeDistances(4, 9, this.transform);

                    if (checkTwoNodeDistances(nodeChoice, lastNodeChosen, this.transform) == nodeChoice)
                    {
                        lastNodeChosen = nodeChoice;
                    }
                }
            }
            return levelGen.GetComponent<Level>().roomData[location][lastNodeChosen];
        }
    */
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
}