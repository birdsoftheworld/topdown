using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHEGun : MonoBehaviour
{
    //public Rigidbody2D bullet;

    public Faction bulletFaction;

    public Transform bulletOrigin;

    public GameObject bulletPrefab;
    //public GameObject explosionPrefab;

    public float bulletSpeed = 20;
    public int bulletDamage = 3;
    public int ammoCap = 3;
    public int ammo;

    public Player player;

    public int jitter;

    public CamFollow cam;

    public bool waveGun = false;

    private ParticleSystem.MainModule main;
    private ParticleSystem.TriggerModule triggers;

    private int ticker;

    private void Start()
    {
        ammo = ammoCap;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollow>();

        waveGun = false;

        main = this.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().main;
        triggers = this.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().trigger;


    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollow>();

        waveGun = false;
    }

    private void FixedUpdate()
    {

        if (ammo == 0 && player.heavyAmmo > 0)
        {
            player.heavyAmmo--;
            ammo++;
        }

        if (waveGun)
        {
            if (Input.GetMouseButton(1))
            {

                //this.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();

                player.waiting = 2;

                if (main.loop == false)
                {
                    main.loop = true;
                }

                ticker++;
                if (ticker == 25)
                {
                    ticker = 0;
                    retarget();
                }

                /*for (int i = 0; i < 3; i++)
                {
                    if (player.driftingTickX > 0)
                    {
                        player.driftingTickX--;
                    }
                    if (player.driftingTickX < 0)
                    {
                        player.driftingTickX++;
                    }
                    if (player.driftingTickY > 0)
                    {
                        player.driftingTickY--;
                    }
                    if (player.driftingTickY < 0)
                    {
                        player.driftingTickY++;
                    }
                }*/


                /*if (this.transform.GetChild(1).rotation.eulerAngles.x != 80 && this.transform.GetChild(1).rotation.eulerAngles.x != 100)
                {
                    this.transform.GetChild(1).Rotate(swingDirection, 0.0f, 0.0f, Space.World);
                }
                else
                {
                    swingDirection = swingDirection * 1;
                    this.transform.GetChild(1).Rotate(swingDirection, 0.0f, 0.0f);
                }*/
            }
            else
            {
                waveGun = false;
                main.loop = false;

                player.waiting = 50;
                player.waiting2 = 75;

                /*Quaternion rotation = new Quaternion();
                rotation.eulerAngles = new Vector3(0f, 90f, 90f);

                this.transform.GetChild(1).rotation = rotation;

                swingDirection = 1f;*/
            }

        }

        else if (player.waiting2 == 0)
        {
            if (Input.GetMouseButton(0))
            {
                waveGun = false;

                if (ammo > 0)
                {

                    ammo--;

                    Vector2 position = bulletOrigin.position;
                    GameObject clone = Instantiate(bulletPrefab, position, bulletOrigin.rotation);
                    clone.gameObject.SetActive(true);

                    Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

                    bullet.bulletSpeed = bulletSpeed;
                    bullet.bulletFaction = bulletFaction;
                    bullet.bulletDamage = bulletDamage;

                    cam.jitter += jitter;

                    this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();

                    player.waiting2 = 25;



                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = Camera.main.nearClipPlane;
                    Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);

                    Vector2 currentPos = this.transform.position;

                    destination = destination - currentPos;

                    Vector3 destinationN = destination.normalized;

                    float angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

                    angle += 180;
                    if (angle > 360f)
                    {
                        angle -= 360;
                    }
                    angle = -1 * ((angle * -1) + 180);
                    if (angle < 0)
                    {
                        angle = 360 + angle;
                    }
                    angle = angle * Mathf.PI / 180;

                    int changeX = (int)(-2 * Mathf.Cos(angle) * player.driftingTickMax);
                    int changeY = (int)(-2 * Mathf.Sin(angle) * player.driftingTickMax);

                    player.driftingTickX += changeX;
                    player.driftingTickY += changeY;

                }
                else
                {
                    //Debug.Log("You need to reload!");
                }
            }
            else if (Input.GetKey(KeyCode.R))
            {
                if (ammo < ammoCap) // && player.heavyAmmo > 0)
                {
                    ammo = ammoCap;
                    player.waiting2 = 100;
                }
            }



           else if (Input.GetMouseButton(1))
            {
                waveGun = true;


                player.body.angularVelocity = 0f;

                retarget();

                ticker = 0;

                this.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            }
        }
    }


    public void retarget()
    {
        int count = triggers.colliderCount;

        while (triggers.colliderCount != 0)
        {
            for (int c = 0; c < count + 1; c++)
            {
                triggers.RemoveCollider(c);
                //Debug.Log(c);
            }
            count = triggers.colliderCount;
        }

        GameObject[] walls;
        walls = GameObject.FindGameObjectsWithTag("Wall");
        for (int a = 0; a < (walls.Length); a++)
        {
            triggers.AddCollider(walls[a].transform);
        }

        GameObject[] explosives;
        explosives = GameObject.FindGameObjectsWithTag("Explosive");
        for (int a = 0; a < (explosives.Length); a++)
        {
            triggers.AddCollider(explosives[a].transform);
        }

        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int b = 0; b < (enemies.Length); b++)
        {
            triggers.AddCollider(enemies[b].transform);
        }

    }
}