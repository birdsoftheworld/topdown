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

    public bool waiterChanged;
    public bool waiter4Changed;

    public int lightAmmoMax;
    public int lightAmmo;

    public int heavyAmmoMax;
    public int heavyAmmo;

    public int healthRestoresMax;
    public int healthRestores;

    public int rocketsMax;
    public int rockets;

    public bool ammoChanged;

    public AmmoTracker ammoCounter;

    public Sprite healthySprite;
    public Sprite injuredSprite;

    public GameObject endMenu;

    bool movingWithIntent = false;

    public int driftingTickMax;
    int driftingTick;

    private void Start()
    {
        driftingTick = 0;

        body = GetComponent<Rigidbody2D>();

        locationCol = GetComponent<Collider2D>();

        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");

        rb2D = GetComponent<Rigidbody2D>();

        slowDown = slowDownMax;
        lightAmmo = 0;
        heavyAmmo = 0;
        rockets = 0;
        healthRestores = 0;

        waiting = 0;
        waiting2 = 0;
        waiting3 = 0;
        waiting4 = 0;

        weapons = new GameObject[3];
        items = new GameObject[1];

        ammoChanged = false;

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

        //UpdateCheck();

        SetWeapon(0);
    }

    private void Update()
    {
        /*if (this.GetComponent<HealthTest>().curHealth <= 0)
        {
            //Debug.Break();
            endMenu = levelGen.GetComponent<ObjectiveController>().endMenu;

            endMenu.SetActive(true);
        }*/

        //if (this.GetComponent<HealthTest>().iFrames > 0)
        //{
            if (this.GetComponent<HealthTest>().justHalved == true)
            {
                this.GetComponent<SpriteRenderer>().sprite = injuredSprite;
                this.GetComponent<HealthTest>().justHalved = false;
            }
        /*    else
            {
                this.GetComponent<SpriteRenderer>().sprite = healthySprite;
            }
        }*/

        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(horizontalMove, verticalMove);
        if(inputDirection.magnitude > 1)
        {
            inputDirection = inputDirection.normalized;
        }

        if (Mathf.Abs(inputDirection.x) + Mathf.Abs(inputDirection.x) > .5)
        {
            movingWithIntent = true;

        }
        else
        {
            movingWithIntent = false;
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
        //ammoCounter.define3(heavyAmmo.ToString());
        //ammoCounter.define4(lightAmmo.ToString());
        //ammoCounter.define5(rockets.ToString());

        ammoChanged = true;
    }

    private void FixedUpdate()
    {
        if (this.GetComponent<HealthTest>().curHealth < this.GetComponent<HealthTest>().maxHealth && healthRestores > 0)
        {
            this.GetComponent<HealthTest>().curHealth++;
            this.GetComponent<HealthTest>().iFrames++;
            healthRestores--;
        }

        if (this.GetComponent<HealthTest>().curHealth <= this.GetComponent<HealthTest>().maxHealth / 3)
        {
            this.transform.GetChild(4).gameObject.GetComponent<ParticleSystem>().Play();
        }

        //if (movingWithIntent == true)
        //{
        if (slowDown < 1)
        {
            body.velocity = body.velocity / 1;
        }
        else
        {


            body.velocity = new Vector2(
                (body.velocity.x / (slowDown - .5f * (1f - Mathf.Abs(inputDirection.x)))), 
                (body.velocity.y / (slowDown - .5f * (1f - Mathf.Abs(inputDirection.y)))));



            //body.velocity = body.velocity / slowDown;
        }
        /*}
        else
        {
            if (slowDown < 2)
            {
                body.velocity = body.velocity / 1;
            }
            else
            {
                body.velocity = body.velocity / (slowDown - 1);
            }

        }*/


        if (slowDown < slowDownMax)
        {
            slowDown++;
        }
        if (slowDown > slowDownMax)
        {
            slowDown--;
        }


        //body.velocity = Vector3.zero;

        if (waiting > 0)
        {
            waiting--;

            if (waiting == 0)
            {
                waiterChanged = true;
            }
        }
        else
        {
            body.AddForce(inputDirection * moveSpeed * 100f);
        }

        if (waiting2 > 0)
        {
            waiting2--;

            if (waiting2 == 0)
            {
                waiterChanged = true;
            }
        }
        if (waiting3 > 0)
        {
            waiting3--;

            if (waiting3 == 0)
            {
                waiterChanged = true;
            }
        }
        if (waiting4 > 0)
        {
            waiting4--;

            if (waiting4 == 0)
            {
                waiter4Changed = true;
            }
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

    public void Reset()
    {
        slowDown = slowDownMax;

        waiting = 0;
        waiting2 = 0;
        waiting3 = 0;
        waiting4 = 0;

        lightAmmo = 0;
        heavyAmmo = 0;
        healthRestores = 0;

        this.transform.position = new Vector2(5.5f, 5.5f);

        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0f;

        this.enabled = true;

        this.GetComponent<HealthTest>().curHealth = this.GetComponent<HealthTest>().maxHealth;

        this.GetComponent<HealthTest>().iFrames = this.GetComponent<HealthTest>().iFrameMax * 5;

        this.GetComponent<HealthTest>().justHalved = false;
        this.GetComponent<HealthTest>().justThirded = false;

        this.GetComponent<HealthTest>().healthBar.SetHealth(this.GetComponent<HealthTest>().curHealth);

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].GetComponent<PlayerCarbine>() != null)
            {
                weapons[i].GetComponent<PlayerCarbine>().ammo = weapons[i].GetComponent<PlayerCarbine>().ammoCap;
            }
            if (weapons[i].GetComponent<PlayerRifle>() != null)
            {
                weapons[i].GetComponent<PlayerRifle>().ammo = weapons[i].GetComponent<PlayerRifle>().ammoCap;
            }
            if (weapons[i].GetComponent<PlayerPistol>() != null)
            {
                weapons[i].GetComponent<PlayerPistol>().ammo = weapons[i].GetComponent<PlayerPistol>().ammoCap;
            }
            if (weapons[i].GetComponent<PlayerCycleLance>() != null)
            {
                weapons[i].GetComponent<PlayerCycleLance>().ammo = weapons[i].GetComponent<PlayerCycleLance>().ammoCap;
            }
            if (weapons[i].GetComponent<PlayerHEGun>() != null)
            {
                weapons[i].GetComponent<PlayerHEGun>().ammo = weapons[i].GetComponent<PlayerHEGun>().ammoCap;
            }

            weapons[i].SetActive(false);
        }

        if (items[0].GetComponent<PlayerCarbine>() != null)
        {
            items[0].GetComponent<PlayerCarbine>().ammo = items[0].GetComponent<PlayerCarbine>().ammoCap;
        }
        if (items[0].GetComponent<PlayerPistol>() != null)
        {
            items[0].GetComponent<PlayerPistol>().ammo = items[0].GetComponent<PlayerPistol>().ammoCap;
        }


        items[0].SetActive(false);

        this.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);

        this.GetComponent<SpriteRenderer>().sprite = healthySprite;

        //UpdateCheck();

        SetWeapon(0);
    }
}
