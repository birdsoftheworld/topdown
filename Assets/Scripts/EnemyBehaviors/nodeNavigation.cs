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

        if (location == player.GetComponent<Player>().location)
        {
            //Debug.Log("flee!");
            Transform closestE = closestExit();
            //Transform

            if (Vector3.Distance(this.transform.position, closestE.position) > .5)
            {
                FaceTarget(closestE.position);
            }

            else
            {
                //MAKE this so that it essentially just makes the target 2 away from the player in a direction determined by nodeDirectionModifier

                //new Vector2 adjustTarge = this.transform.position + Vector3.Up * nodeDirectionModifier.x;

                //FaceTarget(this.transform.position + nodeDirectionModifier);
            }

            rb2D.AddForce(transform.up * speed * -100f);

        }
        else
        {
            //Debug.Log("to cover!");


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

            nodeDirectionModifier = new Vector2(0.0f, 1.0f);
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().downExit == true)
        {
            if (closest == null)
            {
                closest = levelGen.GetComponent<Level>().roomData[location][11];

                nodeDirectionModifier = new Vector2(0.0f, -1.0f);
            }
            else
            {
                float distance2 = Vector3.Distance(this.transform.position, levelGen.GetComponent<Level>().roomData[location][11].transform.position);
                if (distance2 < distance)
                {
                    closest = levelGen.GetComponent<Level>().roomData[location][11];

                    nodeDirectionModifier = new Vector2(0.0f, -1.0f);
                }
            }
            distance = Vector3.Distance(this.transform.position, closest.transform.position);
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().leftExit == true)
        {
            if (closest == null)
            {
                closest = levelGen.GetComponent<Level>().roomData[location][5];

                nodeDirectionModifier = new Vector2(-1.0f, 0.0f);
            }
            else
            {
                float distance2 = Vector3.Distance(this.transform.position, levelGen.GetComponent<Level>().roomData[location][5].transform.position);

                if (distance2 < distance)
                {
                    closest = levelGen.GetComponent<Level>().roomData[location][5];

                    nodeDirectionModifier = new Vector2(-1.0f, 0.0f);
                }
            }
            distance = Vector3.Distance(this.transform.position, closest.transform.position);
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().rightExit == true)
        {
            if (closest == null)
            {
                closest = levelGen.GetComponent<Level>().roomData[location][7];

                nodeDirectionModifier = new Vector2(1.0f, 0.0f);
            }
            else
            {
                float distance2 = Vector3.Distance(this.transform.position, levelGen.GetComponent<Level>().roomData[location][7].transform.position);

                if (distance2 < distance)
                {
                    closest = levelGen.GetComponent<Level>().roomData[location][7];

                    nodeDirectionModifier = new Vector2(1.0f, 0.0f);
                }
            }
            distance = Vector3.Distance(this.transform.position, closest.transform.position);
        }
        return closest;
    }
}