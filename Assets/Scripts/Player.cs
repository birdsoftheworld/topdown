using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Base movement speed of the player")]
    public float moveSpeed = 0.25f;

    private Vector2 inputDirection = Vector2.zero;
    private Rigidbody2D body;


    private Rigidbody2D rb2D;
    private Vector2 destination;

    public int location;

    public Collider2D locationCol;
    private GameObject levelGen;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();

        locationCol = GetComponent<Collider2D>();

        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");

    }

    private void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(horizontalMove, verticalMove);
        if(inputDirection.magnitude > 1)
        {
            inputDirection = inputDirection.normalized;
        }



        ////////////
        rb2D = GetComponent<Rigidbody2D>();


        //destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        destination = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 currentPos = this.transform.position;

        destination = destination - currentPos;

        Vector3 destinationN = destination.normalized;

        float angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);

        transform.rotation = rotation;

        /*if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(closestExit().position);
        }*/

    }

    private void FixedUpdate()
    {
        //body.position = body.position + (inputDirection * moveSpeed);

        body.velocity = body.velocity/3;
        //body.velocity = Vector3.zero;
        body.AddForce(inputDirection * moveSpeed * 100f);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Floor")
        {
            location = col.GetComponent<storeRoomVars>().integer;

            locationCol = col;

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
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().downExit == true)
        {
            if (closest == null) 
            {
                closest = levelGen.GetComponent<Level>().roomData[location][11];
            }
            else
            {
                float distance2 = Vector3.Distance(this.transform.position, levelGen.GetComponent<Level>().roomData[location][11].transform.position);
                if (distance2 < distance)
                {
                    closest = levelGen.GetComponent<Level>().roomData[location][11];
                }
            }
            distance = Vector3.Distance(this.transform.position, closest.transform.position);
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().leftExit == true)
        {
            if (closest == null) {
                closest = levelGen.GetComponent<Level>().roomData[location][5];
            }
            else {
                float distance2 = Vector3.Distance(this.transform.position, levelGen.GetComponent<Level>().roomData[location][5].transform.position);

                if (distance2 < distance)
                {
                    closest = levelGen.GetComponent<Level>().roomData[location][5];
                }
            }
            distance = Vector3.Distance(this.transform.position, closest.transform.position);
        }
        if (locationCol.gameObject.GetComponent<storeRoomVars>().rightExit == true)
        {
            if (closest == null) 
            {
                closest = levelGen.GetComponent<Level>().roomData[location][7];
            }
            else {
                float distance2 = Vector3.Distance(this.transform.position, levelGen.GetComponent<Level>().roomData[location][7].transform.position);

                if (distance2 < distance)
                {
                    closest = levelGen.GetComponent<Level>().roomData[location][7];
                }
            }
            distance = Vector3.Distance(this.transform.position, closest.transform.position);
        }
        return closest;
    }

}
