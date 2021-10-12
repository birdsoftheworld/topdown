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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb2D = GetComponent<Rigidbody2D>();

        target = player;

        targetL = target.transform;

        waiting = 0;

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
        if (location == player.GetComponent<player>().location)
        {
            Debug.Log("flee!");
        }
        else
        {
            Debug.Log("to cover!");
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
            location = col.GetComponent<storeInt>().integer;
        }
    }
}