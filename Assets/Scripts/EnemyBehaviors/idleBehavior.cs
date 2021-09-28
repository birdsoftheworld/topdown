using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleBehavior : MonoBehaviour
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

    public bool waiting;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb2D = GetComponent<Rigidbody2D>();

        target = player;

        targetL = target.transform;

        waiting = false;

    }

    // Update is called once per frame
    void Update()
    {
        //targetL = target.transform;

        //FaceTarget(targetL.position);

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
                //Debug.Log("We found Target!");

                active = true;
                onActivate();

                targetL = player.transform;
            }
            else
            {
                //Debug.Log("I found something else with name = " + hit.collider.name);
            }
        }

        if (active = true && distance2 >= distance * 3)
        {
            active = false;
            onDeactivate();
        }



    }

    void FixedUpdate()
    {
        rb2D.velocity = Vector2.zero;

        if (active == true)
        {
            FaceTarget(targetL.position);
            rb2D.AddForce(transform.up * speed * -2f);
        }
        else
        {
            if (waiting == false)
            {
                setIdleTarget();
                FaceTarget(targetL.position);
                rb2D.AddForce(transform.up * speed * -1f);
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

    private void setIdleTarget()
    {
        if (targetL == player.transform)
        {
            //StartCoroutine(waiter(100));

            float targetDistance = Vector3.Distance(this.transform.position, idleLocations[0].transform.position);
            targetL = idleLocations[0].transform;

            for (int i = 0; i < idleLocations.Length; i++)
            {
                float newDist = Vector3.Distance(this.transform.position, idleLocations[i].transform.position);

                if (targetDistance > newDist)
                {
                    targetDistance = newDist;
                    targetL = idleLocations[i].transform;
                    idleNumber = i;
                }
            }


        }
        else
        {
            //Debug.Log(Vector2.Distance(this.transform.position, targetL.position));

            if (Vector2.Distance(this.transform.position, targetL.position) < 1.1)
            {
                //StartCoroutine(waiter(100));


                idleNumber++;

                if (idleNumber == 3) { idleNumber = 0; }

                targetL = idleLocations[idleNumber].transform;

            }
            else
            {

            }
        }
    }





    IEnumerator waiter(float time)
    {
        //Rotate 90 deg
        //transform.Rotate(new Vector3(90, 0, 0), Space.World);

        //Wait for 4 seconds
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //waiting = true;

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(time);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        waiting = false;

        //Rotate 40 deg
        //transform.Rotate(new Vector3(40, 0, 0), Space.World);

        //Wait for 2 seconds
        //yield return new WaitForSeconds(2);

        //Rotate 20 deg
        //transform.Rotate(new Vector3(20, 0, 0), Space.World);
    }
}