using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emptyBehavior : MonoBehaviour
{

    private GameObject player;

    private Vector2 destination;


    //public HealthTest health;


    public float distance;


    public bool active = false;

    public float speed;
    private Rigidbody2D rb2D;

    private GameObject target;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb2D = GetComponent<Rigidbody2D>();

        target = player;
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget(target.transform.position);

        //if (health.curHealth <= 0)
        //{
        //    Destroy(gameObject);
        //}


        ////////  distance to activate
        float distance2 = Vector3.Distance(this.transform.position, target.transform.position);

        if (distance2 <= distance)
        {

            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, player.transform.position - this.transform.position);
            if (hit.collider.gameObject.tag == "Player")
            {
                //Debug.Log("We found Target!");

                active = true;
                onActivate();

            }
            else
            {
                //Debug.Log("I found something else with name = " + hit.collider.name);
            }
        }

        if (distance2 >= distance * 3)
        {
            active = false;
            onDeactivate();
        }

        if (active == true)
        {

        }
        else
        {

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

}
