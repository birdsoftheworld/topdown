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

    public Vector2 adjustTarge = new Vector2(0,0);

    public Vector2 coverTarge = new Vector2(0, 0);


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
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, player.transform.position - this.transform.position);
            if (hit.collider.gameObject.tag == "Player")
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

        if (location == player.GetComponent<Player>().location || movingToNode == true)
        {
            inCover = false;

            //Debug.Log("flee!");
            Transform closestE = closestExit();
            
            //Transform

            if (Vector3.Distance(this.transform.position, closestE.position) > 1 && movingToNode == false)
            {
                FaceTarget(closestE.position);

                rb2D.AddForce(transform.up * speed * -100f);

                float a1 = this.transform.position.x;
                float b1 = this.transform.position.y;
                float a2 = nodeDirectionModifier.x;
                float b2 = nodeDirectionModifier.y;

                adjustTarge = new Vector2(a1 + a2, b1 + b2);
            }

            else
            {
                FaceTarget(adjustTarge);

                movingToNode = true;

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
                //make them run towards the nearest nodeEntrance until they see player, shoot, and then reassess what cover they want
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

    private Transform findCover()
    {
        Transform closest;
        closest = this.transform;
        int lastNodeChosen = 0;

        if (locationCol.gameObject.GetComponent<storeRoomVars>().upExit == true)
        {
            int nodeChoice = checkTwoNodeDistances(0, 2, this.transform);

            closest = levelGen.GetComponent<Level>().roomData[location][nodeChoice];
            lastNodeChosen = nodeChoice;

            //nodeDirectionModifier = new Vector2(0.0f, 5.0f);
            //Debug.Log("up");
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().downExit == true)
        {
            if (closest == this.transform)
            {
                int nodeChoice = checkTwoNodeDistances(10, 12, this.transform);

                closest = levelGen.GetComponent<Level>().roomData[location][nodeChoice];
                lastNodeChosen = nodeChoice;

            }
            else
            {
                int nodeChoice = checkTwoNodeDistances(10, 12, this.transform);

                if (checkTwoNodeDistances(nodeChoice, lastNodeChosen, this.transform) == nodeChoice)
                {
                    closest = levelGen.GetComponent<Level>().roomData[location][nodeChoice];
                    lastNodeChosen = nodeChoice;
                }
            }
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().leftExit == true)
        {
            if (closest == this.transform)
            {
                int nodeChoice = checkTwoNodeDistances(3, 8, this.transform);

                closest = levelGen.GetComponent<Level>().roomData[location][nodeChoice];
                lastNodeChosen = nodeChoice;

            }
            else
            {
                int nodeChoice = checkTwoNodeDistances(3, 8, this.transform);

                if (checkTwoNodeDistances(nodeChoice, lastNodeChosen, this.transform) == nodeChoice)
                {
                    closest = levelGen.GetComponent<Level>().roomData[location][nodeChoice];
                    lastNodeChosen = nodeChoice;
                }
            }
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().rightExit == true)
        {
            if (closest == this.transform)
            {
                int nodeChoice = checkTwoNodeDistances(4, 9, this.transform);

                closest = levelGen.GetComponent<Level>().roomData[location][nodeChoice];
                lastNodeChosen = nodeChoice;

            }
            else
            {
                int nodeChoice = checkTwoNodeDistances(4, 9, this.transform);

                if (checkTwoNodeDistances(nodeChoice, lastNodeChosen, this.transform) == nodeChoice)
                {
                    closest = levelGen.GetComponent<Level>().roomData[location][nodeChoice];
                    lastNodeChosen = nodeChoice;
                }
            }
        }
        return closest;
    }

    public int checkTwoNodeDistances(int node1, int node2, Transform target)
    {
        //Debug.Log("node1 " + node1);
        //Debug.Log("node2 " + node2);


        float distance2 = Vector3.Distance(target.position, levelGen.GetComponent<Level>().roomData[location][node1].transform.position);
        //distance2 = distance2 - Vector3.Distance(this.transform.position, levelGen.GetComponent<Level>().roomData[location][node1].transform.position);

        float distance3 = Vector3.Distance(target.transform.position, levelGen.GetComponent<Level>().roomData[location][node2].transform.position);
        //distance3 = distance3 - Vector3.Distance(this.transform.position, levelGen.GetComponent<Level>().roomData[location][node2].transform.position);

        if (distance2 < distance3)
        {
            Debug.Log("node1 " + node1);
            return node1;
        }
        else
        {
            Debug.Log("node2 " + node2);
            return node2;
        }

        Debug.Log("fail");
        return node1;
    }

}