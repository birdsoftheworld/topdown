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

    public GameObject[] weapons = new GameObject[5];

    public int dashRechargeMax = 300;
    public int dashRecharge = 300;

    public int slowDownMax = 3;
    public int slowDown = 3;

    public int waiting = 0;
    public int waiting2 = 0;
    public int waiting3 = 0;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();

        locationCol = GetComponent<Collider2D>();

        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");

        rb2D = GetComponent<Rigidbody2D>();

        slowDown = slowDownMax;

        waiting = 0;
        waiting2 = 0;
        waiting3 = 0;
}

    private void Update()
    {
        if (this.GetComponent<HealthTest>().curHealth <= 0)
        {
            Debug.Break();
        }

        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(horizontalMove, verticalMove);
        if(inputDirection.magnitude > 1)
        {
            inputDirection = inputDirection.normalized;
        }

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

        if (Input.GetKeyDown("1"))
        {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            weapons[3].SetActive(false);
            //weapons[4].SetActive(false);
        }
        if (Input.GetKeyDown("2"))
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            weapons[2].SetActive(true);
            weapons[3].SetActive(false);
            //weapons[4].SetActive(false);
        }
        if (Input.GetKeyDown("3"))
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            weapons[3].SetActive(true);
            //weapons[4].SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (dashRecharge == dashRechargeMax)
            {
                //Debug.Log("zooom");
                body.AddRelativeForce(Vector2.down * moveSpeed * 2000f);
                slowDown = 1;
                dashRecharge = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        body.velocity = body.velocity/slowDown;
        if (slowDown < slowDownMax)
        {
            slowDown++;
        }
        //body.velocity = Vector3.zero;

        if (waiting > 0)
        {
            waiting--;
        }
        else
        {
            body.AddForce(inputDirection * moveSpeed * 100f);
        }

        if(dashRecharge < dashRechargeMax)
        {
            dashRecharge++;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Floor")
        {
            location = col.GetComponent<storeRoomVars>().integer;

            locationCol = col;

        }
    }
}
