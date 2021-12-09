using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Base movement speed of the player")]
    public float moveSpeed = 0.25f;

    private Vector2 inputDirection = Vector2.zero;
    public Rigidbody2D body;


    private Rigidbody2D rb2D;
    private Vector2 destination;

    public int location;

    public Collider2D locationCol;
    private GameObject levelGen;

    public GameObject[] weapons;

    public GameObject[] items;


    public int slowDownMax = 3;
    public int slowDown = 3;

    public int waiting = 0;
    public int waiting2 = 0;
    public int waiting3 = 0;
    public int waiting4 = 0;

    public int lightAmmoMax;
    public int lightAmmo;

    public int heavyAmmoMax;
    public int heavyAmmo;

    public int rocketsMax;
    public int rockets;

    public AmmoTracker ammoCounter;

    public Sprite injuredSprite;

    public GameObject endMenu;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();

        locationCol = GetComponent<Collider2D>();

        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");

        rb2D = GetComponent<Rigidbody2D>();

        slowDown = slowDownMax;
        lightAmmo = lightAmmoMax;
        heavyAmmo = heavyAmmoMax;
        rockets = rocketsMax;

        waiting = 0;
        waiting2 = 0;
        waiting3 = 0;
        waiting4 = 0;

        weapons = new GameObject[3];
        items = new GameObject[1];

        for (int i = 0; i < 3; i++)
        {
            if (this.transform.GetChild(0).transform.GetChild(i) != null)
            {
                weapons[i] = this.transform.GetChild(0).transform.GetChild(i).gameObject;
            }
        }
        if (this.transform.GetChild(1).transform.GetChild(0) != null)
        {
            items[0] = this.transform.GetChild(1).transform.GetChild(0).gameObject;
        }
        this.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Update()
    {
        /*if (this.GetComponent<HealthTest>().curHealth <= 0)
        {
            //Debug.Break();
            endMenu = levelGen.GetComponent<ObjectiveController>().endMenu;

            endMenu.SetActive(true);
        }*/
        if (this.GetComponent<HealthTest>().curHealth <= this.GetComponent<HealthTest>().maxHealth / 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = injuredSprite;
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
            SetWeapon(0);
        }
        if (Input.GetKeyDown("2"))
        {
            SetWeapon(1);
        }
        if (Input.GetKeyDown("3"))
        {
            SetWeapon(2);
        }
    }

    private void SetWeapon(int set)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == set)
            {
                weapons[i].SetActive(true);
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }

        if (weapons[set].GetComponent<StoreWeaponVars>().oneHanded == true)
        {
            items[0].SetActive(true);
        }
        else
        {
            items[0].SetActive(false);
        }
    }

    public void UpdateCheck()
    {
        ammoCounter.define3(heavyAmmo.ToString());
        ammoCounter.define4(lightAmmo.ToString());
        ammoCounter.define5(rockets.ToString());
    }

    private void FixedUpdate()
    {
        if (slowDown < 1)
        {
            body.velocity = body.velocity / 1;
        }
        else
        {
            body.velocity = body.velocity / slowDown;
        }
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

        if (waiting2 > 0)
        {
            waiting2--;
        }
        if (waiting3 > 0)
        {
            waiting3--;
        }
        if (waiting4 > 0)
        {
            waiting4--;
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
