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

    public int waiting;

    public int location;

    public Collider2D locationCol;
    private GameObject levelGen;


    public int ammoCap;
    public int ammo;
    public int bulletSpeed;
    public int bulletDamage;
    public GameObject bulletPrefab;

    private Transform chasingTarget;

    public int facingNumber = 0;

    public Transform facingTarge;

    // Start is called before the first frame update
    void Start()
    {

        nearbyOtherRooms = new List<GameObject>();

        ammo = ammoCap;

        player = GameObject.FindGameObjectWithTag("Player");

        facingTarge = player.transform;

        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");

        rb2D = GetComponent<Rigidbody2D>();

        waiting = 0;

        locationCol = GetComponent<Collider2D>();


    }


    // Update is called once per frame
    void Update()
    {
        if (nearbyOtherRooms.Count == 0)
        {
            validExits();
        }

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
            }
            else
            {
            }
        }
        if (active == true && distance2 >= distance * 3)
        {
            active = false;
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
        //facingTarge = nearbyOtherRooms[facingNumber].transform;

        Vector3 dirFromAtoB = (nearbyOtherRooms[facingNumber].transform.position - this.transform.position).normalized;
        float dotProd = Vector3.Dot(dirFromAtoB, this.transform.forward);

        //Debug.Log(dotProd);

        //if (dotProd == 0)
        //Debug.Log(Mathf.Round(this.transform.rotation.z)); // ADD BEHAVIOR SO IT COMPLETE FULL 90 DEGREES
        if (this.transform.rotation.z < 1)
        {
            //Debug.Log(nearbyOtherRooms[facingNumber].transform.position);
            //Debug.Log(facingNumber);
            //Debug.Log(nearbyOtherRooms.Count);
            waiting = 0;

            if (facingNumber == nearbyOtherRooms.Count - 1)
            {
                facingNumber = 0;
            }
            else
            {
                facingNumber++;
            }
            facingTarge = nearbyOtherRooms[facingNumber].transform;
        }
        else
        {
            facingTarge = nearbyOtherRooms[facingNumber].transform;
        }
        
        Vector3 vectorToTarget = facingTarge.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
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
        Debug.Log("ping");
        return closest.transform;
    }

    private void validExits()
    {

        GameObject[] centralNodes;

        centralNodes = GameObject.FindGameObjectsWithTag("NodeCenter");

        Vector3 position = this.transform.position;

        Transform meCenter = this.transform;

        foreach (GameObject go2 in centralNodes)
        {
            //Debug.Log(Vector2.Distance(meCenter.position, go2.transform.position));
            if (Vector2.Distance(meCenter.position, go2.transform.position) > 10 && Vector2.Distance(meCenter.position, go2.transform.position) < 12)
            {
                nearbyOtherRooms.Add(go2);
                Debug.Log(go2.transform.position);
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Floor")
        {
            locationCol = col;

            location = col.GetComponent<storeRoomVars>().integer;
        }
    }
}